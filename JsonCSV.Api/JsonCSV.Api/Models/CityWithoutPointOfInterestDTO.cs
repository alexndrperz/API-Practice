namespace JsonCSV.Api.Models
{
	public class CityWithoutPointOfInterestDTO
	{
		public int id { get; set; }
		public string? name { get; set; } = string.Empty;
		public string? description { get; set; }
	}
}
