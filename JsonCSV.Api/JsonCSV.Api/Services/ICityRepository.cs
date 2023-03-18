﻿using JsonCSV.Api.Entities;

namespace JsonCSV.Api.Services
{
	public interface ICityRepository
	{
		Task<IEnumerable<City>> GetCities();
		Task<City?> GetCity(int cityId, bool includePointsInt);
		Task<bool> CityExistsVerification(int cityId);
		Task<IEnumerable<PointOfInterest>> GetPointOfInterests(int cityId);
		Task<PointOfInterest> GetPointOfInterest(int cityId, int pointOfInterestId);	
	} 
}