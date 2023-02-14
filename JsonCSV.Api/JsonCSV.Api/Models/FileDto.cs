namespace JsonCSV.Api.Models
{
	public class FileDto
	{
		public int id { get; set; }
		public string? name { get; set; } = string.Empty;
		public string? description { get; set; }	

		public int NumbersOfInterestPoints { 
			get
			{
				return InterestPoints.Count; 
			} 
		}	

		public ICollection<DateDto> InterestPoints { get; set; } = new List<DateDto>();		

	}
}
