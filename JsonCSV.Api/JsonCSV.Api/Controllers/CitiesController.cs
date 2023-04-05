using AutoMapper;
using JsonCSV.Api.DbContexts;
using JsonCSV.Api.Entities;
using JsonCSV.Api.Models;
using JsonCSV.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace JsonCSV.Api.Controllers
{
	
	[ApiController]
	[ApiVersion("1.0")]
	//[Authorize(Policy ="MustBeAdmin")]
	[Route("api/cities/v{version:apiVersion}")]
	public class CitiesController : ControllerBase
	{
		private const int maxSize = 10;
		private readonly ICityRepository _cityEntities;
		private readonly IMapper _mapper;

		public CitiesController(CitiesDataStore cities, ICityRepository cityRepository, IMapper mapper) {

			_cityEntities = cityRepository ?? throw new ArgumentException(nameof(cityRepository));
			_mapper = mapper ?? throw new ArgumentException(nameof(mapper));

		}

		///
		[HttpGet]
		public async Task<IActionResult> getValues(bool includePD, [FromQuery] string? name, string? search, int pageNumber=1, int pageSize = 10)
		{
			if (pageSize > maxSize) { 
				pageSize = maxSize;	
			}

			var (cityEntities, paginationData) = await _cityEntities.GetCities(pageNumber,pageSize,includePD,name,search);
			if (cityEntities.Count() == 0)
			{
				return NotFound();
			}

			Response.Headers.Add("PaginationData",JsonSerializer.Serialize(paginationData));	

			if (includePD)
			  {
				return Ok(_mapper.Map<IEnumerable<CitiesDTO>>(cityEntities));
			}
			return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDTO>>(cityEntities));

		}


		/// <summary>
		/// Obtener una ciudad basada en el id
		/// </summary>
		/// <param name="id">ID de la ciudad</param>
		/// <param name="includePD">Valor Booleano de si deberia incluir los puntos de interest o no</param>
		/// <returns>Un json con la ciudad y los puntos de interes si tiene y se le especifica que tenga</returns>
		/// <response code="404">No se encuentra la ciudad</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]	
		[HttpGet("{id}")] 
		public async Task<IActionResult> getValue(int id, bool includePD = false)
		{
			var City = await _cityEntities.GetCity(id, includePD);
			if (City == null)
			{
				return NotFound();
			}
			if (includePD)
			{
				return Ok(_mapper.Map<CitiesDTO>(City));
			}

			return Ok(_mapper.Map<CityWithoutPointOfInterestDTO>(City));
		}

		[HttpPost]
		public async Task<ActionResult<CityWithoutPointOfInterestDTO>> AddCity(CityWithoutPointOfInterestDTO city)
		{
			var cityObject = _mapper.Map<City>(city);
			await _cityEntities.AddCity(cityObject);
			await _cityEntities.SaveData();
			var cityObjectForReading = _mapper.Map<CityWithoutPointOfInterestDTO>(cityObject);

			var uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? -1);
			var baseUri = await _cityEntities.BaseURI(uriBuilder);
			var Uri = $"{baseUri}api/cities/{cityObjectForReading.id}";
			return Created(Uri, cityObjectForReading);
		}

		[HttpDelete("{cityId}")]
		public async Task<ActionResult> DeleteCity(int cityid)
		{
			if(!await _cityEntities.CityExistsVerification(cityid))
			{
				return NotFound();
			}
			var cityToDelete = await _cityEntities.GetCity(cityid, true);
			_cityEntities.DeletingResources(cityToDelete);
			await _cityEntities.SaveData();
			return NoContent();
		}
	}


}
