using AutoMapper;
using LearningWebApi.DtoModels;
using LearningWebApi.Models;

namespace LearningWebApi.AutoMapperProfiles

{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<City,CityResponse>().ReverseMap();
            CreateMap<CityRequestToCreate, City>().ReverseMap();
            CreateMap<CityRequestToUpdate, City>().ReverseMap();
        }  
    }
}
