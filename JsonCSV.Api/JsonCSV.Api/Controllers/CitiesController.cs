using AutoMapper;
using JsonCSV.Api.Models;
using JsonCSV.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace JsonCSV.Api.Controllers
{
	[ApiController] 
	[Route("api/cities")] 
	public class CitiesController : ControllerBase 
	{
		private readonly ICityRepository _cityEntities;	
		private readonly IMapper _mapper;	
		public CitiesController(CitiesDataStore cities, ICityRepository cityRepository, IMapper mapper) {

			_cityEntities = cityRepository ?? throw new ArgumentException(nameof(cityRepository));
			_mapper = mapper ?? throw new ArgumentException(nameof(mapper));
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDTO>>> getValues()
		{
			var cityEntities = await _cityEntities.GetCities();
			return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDTO>>(cityEntities));
        }



		[HttpGet("{id}")] // Get just a element based in the id
		public async Task<IActionResult> getValue(int id, bool includePD = false)
		{
			var City = await _cityEntities.GetCity(id, includePD);
			if (City == null)
			{
				return NotFound();
			}
			if(includePD)
			{
				return Ok(_mapper.Map<CitiesDTO>(City));
			}

			return Ok(_mapper.Map<CityWithoutPointOfInterestDTO>(City));
		}
	}


}
