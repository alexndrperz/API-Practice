using AutoMapper;

namespace JsonCSV.Api.Profiles
{
	public class CityProfile : Profile
	{
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointOfInterestDTO>();
			CreateMap<Models.CityWithoutPointOfInterestDTO, Entities.City>();
			CreateMap<Entities.City, Models.CitiesDTO>();
            CreateMap<Entities.City, Models.PointOfInterestCreatorDTO>();   
        }
    }
}
