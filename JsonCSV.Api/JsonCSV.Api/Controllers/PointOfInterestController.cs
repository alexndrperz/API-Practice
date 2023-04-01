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
		public async  Task<ActionResult> InterestPointUpdater( int cityid, int pointOfInterestID, PointOfInterestUpdaterDTO pointOfInterest)
		{
			if (!await _cityRepository.CityExistsVerification(cityid))
			{
				return NotFound();
			}

			var pointOfInterestStored = await _cityRepository.GetPointOfInterest(cityid, pointOfInterestID);
			if (pointOfInterestStored == null)
			{
				return NotFound();
			}

			_mapper.Map(pointOfInterest, pointOfInterestStored);

			await _cityRepository.SaveData();

			return NoContent();
		}

		[HttpPatch("{pointOfInterestId}")]
		public async  Task<ActionResult> PartiallyUpdateOfData(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestUpdaterDTO> patchObject)
		{
			if (!await _cityRepository.CityExistsVerification(cityId))
			{
				return NotFound();
			}

			var PIEntity = await _cityRepository.GetPointOfInterest(cityId, pointOfInterestId);
			if(PIEntity == null)
			{
				return NotFound(new { message = "asdasd" });
			}

			var pointOfInterestStored = _mapper.Map<PointOfInterestUpdaterDTO>(PIEntity);

			if (pointOfInterestStored == null) {
				return NotFound();
			}

			patchObject.ApplyTo(pointOfInterestStored, ModelState);

            if (!ModelState.IsValid)
            {
				return BadRequest(ModelState);
            }

			if(!TryValidateModel(pointOfInterestStored)) { 
				return BadRequest(ModelState);
			}

			await _cityRepository.SaveData();	

			return NoContent();
        }

		[HttpDelete("{pointsOfInterestID}")]
		public async Task<ActionResult> DeleteResources(int cityId, int pointsOfInterestID) 
		{
			if (!await _cityRepository.CityExistsVerification(cityId))
			{
				return BadRequest();
			}

			var pointofinterest = await _cityRepository.GetPointOfInterest(cityId, pointsOfInterestID);
			if (pointofinterest == null) {
				return BadRequest();
			}

			_cityRepository.DeletingResources(pointofinterest);
			await _cityRepository.SaveData();

			return NoContent();

		}

	} 
}
