using AutoMapper;
using LearningWebApi.Models.DomainModels;
using LearningWebApi.Models.DtoModels;
using LearningWebApi.Models.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace LearningWebApi.AutoMapperProfiles

{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<City,CityResponse>().ReverseMap();
            CreateMap<CityRequestToCreate, City>().ReverseMap();
            CreateMap<CityRequestToUpdate, City>().ReverseMap();
            CreateMap<AcountRequestToRegister, ApplicationUser>()
                .ForMember(option => option.PersonName, option => option.MapFrom(x => x.Name))
                .ForMember(option => option.UserName, option => option.MapFrom(x => x.Email));
        }  
    }
}
