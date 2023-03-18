using AutoMapper;
using JsonCSV.Api.Models;
using JsonCSV.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace JsonCSV.Api.Controllers
{



	[Route("api/cities/{cityid}/pointsofinterest")]
	[ApiController]
	public class PointOfInterestController : ControllerBase
	{
		private readonly ILogger<PointOfInterestController> _logger;
		private readonly IMailService _emailService;
		private readonly CitiesDataStore _citiesData;
		private readonly ICityRepository _cityRepository;
		private readonly IMapper _mapper;
		public PointOfInterestController(ILogger<PointOfInterestController> logger, IMailService emailService, 
			CitiesDataStore citiesDataStore, ICityRepository cityRepository, IMapper mapper)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
			_cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));	
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PointOfInterestController>>> GetInterestPoints(int cityid)
		{
            if (!await _cityRepository.CityExistsVerification(cityid))
            {
				_logger.LogError($"La ciudad {cityid} no existe");
				return NotFound();
            }
            var pointOfInterestOFCity  = await _cityRepository.GetPointOfInterests(cityid);	
			return Ok(_mapper.Map<IEnumerable<PointOfInterestDTO>>(pointOfInterestOFCity));

		}

		[HttpGet("{interestpointsID}")]
		public async Task<ActionResult<PointOfInterestController>> GetInterestPoints(int cityid, int interestpointsID)
		{
			if(!await _cityRepository.CityExistsVerification(cityid))
			{
				_logger.LogError($"La ciudad {cityid} no existe");
				return NotFound();
			}
			var pointOfInterest =  await _cityRepository.GetPointOfInterest(cityid, interestpointsID);
			
			if (pointOfInterest == null) { 
				return NotFound();
			}
			return Ok(_mapper.Map<CitiesController>(pointOfInterest));	
			
			//var file = _citiesData.Cities.FirstOrDefault(c => c.id == cityid);
			//if (file == null) {
			//	_logger.LogCritical($"La ciudad #{cityid} no se encuentra en nuestra base de datos");
			//	return NotFound();
			//}

			//var InterestPoints = file.InterestPoints.FirstOrDefault(c => c.id == interestpointsID);
			//if (InterestPoints == null)
			//{
			//	return NotFound();
			//}
			//return Ok(InterestPoints);
		}
		[HttpPost]
		public ActionResult<PointOfInterestController> CreateInterestPoint(int cityid, PointOfInterestCreatorDTO pointOfInterest)
		{
			var citiesDataStore = _citiesData.Cities.FirstOrDefault(c => c.id == cityid);
			if (citiesDataStore == null) {
				_logger.LogCritical($"La ciudad #{cityid} no se encuentra en nuestra base de datos");
				return NotFound();
			}
			

			var mx_interestPoint = _citiesData.Cities.SelectMany(c => c.InterestPoints).Max(p => p.id);
			var finalInterestPoint = new PointOfInterestDTO()
			{
				id = ++mx_interestPoint,
				name = pointOfInterest.name,
				description = pointOfInterest.description
			};

			citiesDataStore.InterestPoints.Add(finalInterestPoint);

			var uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? -1);
			if (uriBuilder.Uri.IsDefaultPort)
			{
				uriBuilder.Port = -1;
			}

			var baseUri = uriBuilder.Uri.AbsoluteUri;

			var val = $"{baseUri}api/cities/{cityid}/pointsofinterest";

			return Created(val, finalInterestPoint);

		}

		[HttpPut("{pointOfInterestID}")]
		public ActionResult InterestPointUpdater(int cityId, int pointOfInterestID, PointOfInterestUpdaterDTO pointOfInterest)
		{
			var citiesStored = _citiesData.Cities.FirstOrDefault(c => c.id == cityId);
			if (citiesStored == null)
			{
				_logger.LogCritical($"La ciudad #{cityId} no se encuentra en nuestra base de datos");
				return NotFound();
			}


			var pointOfInterestStored = citiesStored.InterestPoints.FirstOrDefault(c => c.id == pointOfInterestID);
			if (pointOfInterestStored == null)
			{
				return NotFound();
			}

			pointOfInterestStored.name = pointOfInterest.name; 
			pointOfInterestStored.description = pointOfInterest.description;

			return NoContent();
		}

		[HttpPatch("{pointOfInterestId}")]
		public ActionResult PartiallyUpdateOfData(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestUpdaterDTO> patchObject)
		{
			var citiesStored = _citiesData.Cities.FirstOrDefault(c => c.id == cityId);
            if (citiesStored == null)
            {
				return NotFound();
            }
			var pointOfInterestStored = citiesStored.InterestPoints.FirstOrDefault(p => p.id == pointOfInterestId);
			if (pointOfInterestStored == null) {
				return NotFound();
			}

			// Con el Updater DTO se evita cambios en campos indeseados como la id en este caso.s
			var PatchInterestPoints = new PointOfInterestUpdaterDTO()
			{
				name = pointOfInterestStored.name,
				description = pointOfInterestStored.description
			};

			patchObject.ApplyTo(PatchInterestPoints, ModelState);

			pointOfInterestStored.name = PatchInterestPoints.name;	
			pointOfInterestStored.description = PatchInterestPoints.description;

            if (!ModelState.IsValid)
            {
				return BadRequest(ModelState);
            }
			if(!TryValidateModel(pointOfInterestStored)) { 
				return BadRequest(ModelState);
			}
			return NoContent();
        }

		[HttpDelete("{pointsOfInterestID}")]
		public ActionResult DeleteResources(int cityId, int pointsOfInterestID) 
		{
			var citiesStored = _citiesData.Cities.FirstOrDefault(c => c.id == cityId);
			if (citiesStored == null)
			{
				return NotFound();
			}
			var pointOfInterestStored = citiesStored.InterestPoints.FirstOrDefault(p => p.id == pointsOfInterestID);
			if (pointOfInterestStored == null)
			{
				return NotFound();
			}

			citiesStored.InterestPoints.Remove(pointOfInterestStored);
			_emailService.SendEmail();
			return NoContent();
		}

	} 
}
