using JsonCSV.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JsonCSV.Api.Controllers
{
    [ApiController] // Speed up the debugging process
	[Route("api/cities")] // Use of Route
	public class ContExample : ControllerBase // Just the Controller class has a lot of innecesary methods for views
	{
		[HttpGet/*("table")*/] // Example of get method (the comment is just a form to route the api)
		public ActionResult<IEnumerable<FileStored>> getValues()
		{
			return Ok(FileStored.Current.Cities);
		}



		[HttpGet("{id}")] // Get just a element based in the id
		public ActionResult<FileDto> getValue(int id)
		{ 
			var returnValue = new JsonResult(FileStored.Current.Cities.FirstOrDefault(c => c.id == id));
			if (returnValue.Value == null) { 
				return NotFound(); // Condition in case that the value isn`t found
			}
			
			return Ok(returnValue.Value);
		}
	}
}
