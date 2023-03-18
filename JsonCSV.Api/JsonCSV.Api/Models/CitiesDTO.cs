namespace JsonCSV.Api.Models
{
	public class CitiesDTO
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

		public ICollection<PointOfInterestDTO> InterestPoints { get; set; } = new List<PointOfInterestDTO>();		

	}
}
