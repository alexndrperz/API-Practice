using JsonCSV.Api.Entities;
using JsonCSV.Api.Models;

namespace JsonCSV.Api.Services
{
	public interface ICityRepository
	{
		Task<IEnumerable<City>> GetCities(bool includePD);
		Task<object> Validate(string username, string passwordHashed);
		Task<City?> GetCity(int cityId, bool includePointsInt);
		Task<(IEnumerable<City>, Metadata)> GetCities(int pageNumber = 1, int pageSize = 10, bool includePD = false, string? name = null, string? search = null);
		Task<bool> CityExistsVerification(int cityId);
		Task<IEnumerable<PointOfInterest>> GetPointOfInterests(int cityId);
		Task<PointOfInterest> GetPointOfInterest(int cityId, int pointOfInterestId);
		Task AddInterestPoints(int cityid, PointOfInterest pointOfInterest);
		void DeletingResources(PointOfInterest pointOfInterest);
		void DeletingResources(City City);
		Task AddCity(City city);	
		Task<bool> SaveData();
		Task<string> BaseURI(UriBuilder uriBuilder);
	} 
}
