using JsonCSV.Api.DbContexts;
using JsonCSV.Api.Entities;
using JsonCSV.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace JsonCSV.Api.Services
{
	public class CityInfoRepository : ICityRepository
	{
		private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext cityInfoContext)
        {
			_context = cityInfoContext;
        }

		public async Task<object> Validate(string username, string password)
		{
			var obj = new UsersIdentification();
			var passwordHashed = obj.SetPassword(password);

			bool result = false;
			var user =   _context.usersIdentification.FirstOrDefault(u => u.UserName == username);
			if (user != null)
			{
				if (passwordHashed == user.PasswordHash)
				{
					result = true;
					return new { status = result, userId = user.Id, username = user.Name, role = user.Role };
				}
			}
			return new {status = result};
		}

		public async Task<(IEnumerable<City>?, Metadata)> GetCities(int pageNumber, int pageSize, bool includePD = false, 
			string? name = null, string? search = null) 
		{
			var collection = _context.Cities as IQueryable<City>;
			

			if (!string.IsNullOrEmpty(name))
			{
				collection = collection.Where(c => c.Name.ToLower().Trim() == name.ToLower().Trim()).Include(c => c.InterestPoints).OrderBy(c => c.Name);
			}

			if (!string.IsNullOrEmpty(search))
			{
				collection = collection.Where(c => c.Name.ToLower().Contains(search) || c.Description != null && c.Description.ToLower().Contains(search));
			}

			int totalItems= await collection.CountAsync();
			Metadata metadata = new Metadata(pageNumber, pageSize, totalItems);
			List<City>? collectionFinal = new List<City>();


			if (includePD == true)
			{
				 collectionFinal = await collection.OrderBy(c => c.Name)
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
				.Include(c => c.InterestPoints)	
				.ToListAsync();
			}
			collectionFinal = await collection.OrderBy(c => c.Name)
				.Skip(pageSize *(pageNumber-1))
				.Take(pageSize)
				.ToListAsync();

			return (collectionFinal, metadata);

		}
		public async Task<IEnumerable<City>> GetCities(bool includePD)
		{
            if (includePD)
            {
				return await _context.Cities.Include(c => c.InterestPoints).ToListAsync();
            }
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();	
		}

		public async Task<City?> GetCity(int cityId, bool includePointsInt)
		{
			if (includePointsInt) { 
				return await _context.Cities.Include(c => c.InterestPoints).Where(c => c.Id  == cityId).FirstOrDefaultAsync();	
			}
			return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
		}

		public async Task<bool> CityExistsVerification(int cityId )
		{
			return await _context.Cities.AnyAsync(c => c.Id == cityId);
		}


		public async Task<IEnumerable<PointOfInterest>> GetPointOfInterests(int cityId)
		{
			return await _context.pointOfInterests.Where(c => c.CityId == cityId).ToListAsync();
		}

		public async Task<PointOfInterest> GetPointOfInterest(int cityId, int pointOfInterestId)
		{
			return await _context.pointOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefaultAsync();
		}

		public async Task AddInterestPoints(int cityid, PointOfInterest pointOfInterest)
		{
			var city = await GetCity(cityid, false);
			if (city != null) {
				city.InterestPoints.Add(pointOfInterest);
			}
		}

		public async Task<bool> SaveData()
		{
			return (await _context.SaveChangesAsync() >= 0);
		}

		public async Task<string> BaseURI(UriBuilder uriBuilder)
		{

			if (uriBuilder.Uri.IsDefaultPort)
			{
				uriBuilder.Port = -1;
			}

			var baseUri =  uriBuilder.Uri.AbsoluteUri;
			return baseUri;
		}

		public void DeletingResources(PointOfInterest pointOfInterest)
		{
			_context.Remove(pointOfInterest);
		}

		public async Task AddCity(City city)
		{
			_context.Cities.Add(city);
		}

		public void DeletingResources(City City)
		{
			_context.Remove(City);
		}
	}
}
