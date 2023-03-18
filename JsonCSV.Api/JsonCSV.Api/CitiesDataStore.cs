namespace JsonCSV.Api.Models
{
	public class CitiesDataStore
	{

		public List<CitiesDTO> Cities { get; set; }

		// public static CitiesDataStore Current { get; } = new CitiesDataStore();
		public CitiesDataStore()
		{
			Cities = new List<CitiesDTO>()
			{
				new CitiesDTO()
				{
					id = 1,
					name= "Test1",
					description= "Test1",
					InterestPoints = new List<PointOfInterestDTO>()
					{
						new PointOfInterestDTO()
						{
							id = 1,
							description = "asdaal",
							name = "Great Cathedral"
						},
						new PointOfInterestDTO()
						{
							id= 2,
							name = "Live e toi"
						}
					}
				},
				new CitiesDTO()
				{
					id = 2,
					name= "Test2",
					description= "Test2"
				},
				new CitiesDTO() {
					id = 3,
					name = "Hes",
					description = "asdasd",
					InterestPoints = new List<PointOfInterestDTO>()
					{
						new PointOfInterestDTO()
						{
							id = 1,
							description = "asdaal",
							name = "Great Cathedral"
						},
						new PointOfInterestDTO() 
						{
							id= 2,	
							name = "Live e toi"
						}
					}
				}
			};
		}
	}
}

