namespace JsonCSV.Api.Models
{
	public class FileStored
	{

		public List<FileDto> Cities { get; set; }

		public static FileStored Current { get; } = new FileStored();
		public FileStored()
		{
			Cities = new List<FileDto>()
			{
				new FileDto()
				{
					id = 1,
					name= "Test1",
					description= "Test1",
					InterestPoints = new List<DateDto>()
					{
						new DateDto()
						{
							id = 1,
							description = "asdaal",
							name = "Great Cathedral"
						},
						new DateDto()
						{
							id= 2,
							name = "Live e toi"
						}
					}
				},
				new FileDto()
				{
					id = 2,
					name= "Test2",
					description= "Test2"
				},
				new FileDto() {
					id = 3,
					name = "Hes",
					description = "asdasd",
					InterestPoints = new List<DateDto>()
					{
						new DateDto()
						{
							id = 1,
							description = "asdaal",
							name = "Great Cathedral"
						},
						new DateDto() 
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

