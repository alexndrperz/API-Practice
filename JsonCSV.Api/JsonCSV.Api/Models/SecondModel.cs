namespace JsonCSV.Api.Models
{
	public class SecondModel
	{
		
		public List<JSONModel> Cities { get; set; }

		public static SecondModel Current { get; } = new SecondModel();
		public SecondModel()
		{
			Cities = new List<JSONModel>()
			{
				new JSONModel()
				{
					id = 2,
					name= "Test1",
					description= "Test1"
				},
				new JSONModel()
				{
					id = 2,
					name= "Test2",
					description= "Test2"
				},
				new JSONModel() {
					id = 3,
					name = "Hes",
					description = "asdasd"
				}
			};
		}
	}
}
