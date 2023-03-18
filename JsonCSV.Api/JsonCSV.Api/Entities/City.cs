using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonCSV.Api.Entities
{
	public class City
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

		public int Id { get; set; }

		[Required]
		[MaxLength(50)]		
		
		public string Name { get; set; }
		[Required]
		[MaxLength(200)]
		public string? Description { get; set; }
		public ICollection<PointOfInterest> InterestPoints { get; set; } = new List<PointOfInterest>();

		public City(string name) { 
			Name = name;
		}	
	}
}
