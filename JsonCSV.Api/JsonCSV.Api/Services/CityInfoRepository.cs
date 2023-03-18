﻿using JsonCSV.Api.DbContexts;
using JsonCSV.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace JsonCSV.Api.Services
{
	public class CityInfoRepository : ICityRepository
	{
		private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext cityInfoContext)
        {
			_context = cityInfoContext;
        }
        public async Task<IEnumerable<City>> GetCities()
		{
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

	}
}