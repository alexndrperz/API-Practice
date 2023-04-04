using Microsoft.EntityFrameworkCore;
using JsonCSV.Api.Entities;

namespace JsonCSV.Api.DbContexts
{
	public class CityInfoContext : DbContext
	{
		public DbSet<City> Cities { get; set; } = null!;
		public DbSet<PointOfInterest> pointOfInterests { get; set; } = null!;
		public DbSet<UsersIdentification> usersIdentification { get; set; }	

		public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { 
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			UsersIdentification yy = new UsersIdentification();	
			modelBuilder.Entity<UsersIdentification>()
				.HasData(
				new UsersIdentification
				{
					Id = 1,	
					UserName = "Alan",
					Name = "asddd",
					PasswordHash = yy.SetPassword("12345"),
					Role = "Admin"
				},
				new UsersIdentification
				{
					Id = 2,
					Name = "asddd",
					UserName = "Raul",
					PasswordHash = yy.SetPassword("12745"),
					Role = "Reader"
				},
				new UsersIdentification
				{
					Id=3,	
					UserName = "wwww",
					Name = "asddd",
					PasswordHash = yy.SetPassword("123w45"),
					Role = "Admin"
				}
			);

			modelBuilder.Entity<City>()
			   .HasData(

			  new City("Antwerp")
			  {
				  Id = 2,
				  Description = "The one with the cathedral that was never really finished."
			  },
			  new City("Paris")
			  {
				  Id = 3,
				  Description = "The one with that big tower."
			  });
		}
	}
}
