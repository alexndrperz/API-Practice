using JsonCSV.Api.Entities;
using JsonCSV.Api.Models;

namespace JsonCSV.Api.Services
{
	public interface ICityRepository
	{
		Task<IEnumerable<City>> GetCities(bool includePD);
		Task<City?> GetCity(int cityId, bool includePointsInt);
		Task<bool> CityExistsVerification(int cityId);
		Task<IEnumerable<PointOfInterest>> GetPointOfInterests(int cityId);
		Task<PointOfInterest> GetPointOfInterest(int cityId, int pointOfInterestId);
		Task AddInterestPoints(int cityid, PointOfInterest pointOfInterest);
		Task<bool> SaveData();
		Task<string> BaseURI(UriBuilder uriBuilder);
	} 
}
