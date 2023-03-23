using AutoMapper;
using JsonCSV.Api.Entities;
using JsonCSV.Api.Models;
using JsonCSV.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
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
			var vsf = _mapper.Map<PointOfInterestDTO>(pointOfInterest);
			return Ok(vsf);	
		}
		[HttpPost]
		public async Task<ActionResult<PointOfInterestDTO>> CreateInterestPoint(int cityid, PointOfInterest pointOfInterest)
		{
            if (!await _cityRepository.CityExistsVerification(cityid)) 
            {
                return NotFound();	
            }

			var finalPoint = _mapper.Map<PointOfInterest>(pointOfInterest);
			await _cityRepository.AddInterestPoints(cityid, pointOfInterest);
			await _cityRepository.SaveData();
			var pointCreated = _mapper.Map<PointOfInterestDTO>(finalPoint);
			var uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? -1);
			var baseUri = await  _cityRepository.BaseURI(uriBuilder);

			var Uri = $"{baseUri}api/cities/{cityid}/pointsofinterest";

			return Created(Uri, pointCreated);
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
