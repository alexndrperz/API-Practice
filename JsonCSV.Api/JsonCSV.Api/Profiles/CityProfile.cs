using AutoMapper;

namespace JsonCSV.Api.Profiles
{
	public class CityProfile : Profile
	{
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointOfInterestDTO>();
            CreateMap<Entities.City, Models.CitiesDTO>();
        }
    }
}
