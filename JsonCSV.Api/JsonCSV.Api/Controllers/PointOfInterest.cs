using JsonCSV.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JsonCSV.Api.Controllers
{
	[Route("api/cities/{cityid}/pointsofinterest")]
	[ApiController]
	public class PointOfInterest : ControllerBase
	{
		[HttpGet]
		public ActionResult<IEnumerable<PointOfInterest>> GetInterestPoints(int cityid)
		{
			var file  = FileStored.Current.Cities.FirstOrDefault(c => c.id == cityid);
			
			if (file  == null) { 
				return NotFound();
			}
			if (file.InterestPoints.Count == 0)
			{
				return NotFound();
			}


			return Ok(file.InterestPoints);
		}

		[HttpGet("{interestpointsID}")]
		public ActionResult<PointOfInterest> GetInterestPoints(int cityid, int interestpointsID)
		{
			var file = FileStored.Current.Cities.FirstOrDefault(c => c.id == cityid);
			if (file == null) {
				return NotFound();
			}

			var InterestPoints = file.InterestPoints.FirstOrDefault(c => c.id == interestpointsID);
			if (InterestPoints == null)
			{
				return NotFound();
			}



			return Ok(InterestPoints);
		}
	} 
}
