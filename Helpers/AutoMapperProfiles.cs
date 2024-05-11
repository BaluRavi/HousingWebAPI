using AutoMapper;
using HousingWebAPI.Dtos;
using HousingWebAPI.Models;

namespace HousingWebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
