
using api.Dto;
using AutoMapper;
using repository.Entities;

namespace api.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SignInDto, User>();
           
        }
    }
}