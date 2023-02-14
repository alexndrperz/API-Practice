using JsonCSV.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace JsonCSV.Api.Controllers
{
	[ApiController] // Speed up the debugging process
	[Route("api/cities")] // Use of Route
	public class ContExample : ControllerBase // Just the Controller class has a lot of innecesary methods for views
	{
		[HttpGet/*("table")*/] // Example of get method (the comments are just a form to route the api)
		public JsonResult getValues()
		{
			var stat = new JsonResult(SecondModel.Current.Cities);
			stat.StatusCode= 200;	


			return new JsonResult(SecondModel.Current.Cities);
		}

		[HttpGet("{id}")] // Get just a element based in the id
		public ActionResult<JSONModel> getValue(int id) 
		{ 
			var cityReturned = new JsonResult(SecondModel.Current.Cities.FirstOrDefault(c=> c.id == id)); // The method  iterates the seconModel and returns the id that is equal, or one is are two equals ids
			return Ok(cityReturned);

		}
	}
}
