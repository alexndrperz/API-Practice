using AutoMapper;
using JsonCSV.Api.Entities;
using JsonCSV.Api.Models;

namespace JsonCSV.Api.Profiles
{
	public class PointOfInterestProfile : Profile
	{
		public PointOfInterestProfile() {
			CreateMap<PointOfInterest, PointOfInterestDTO>();
			CreateMap<PointOfInterestCreatorDTO, PointOfInterest>();
		}	
	}
}
