using AutoMapper;
using Data.Api.Models;
using Domain.Api.ViewModels.Users;

namespace Logic.Api.Helpers.Mappers
{
    public class MappingModels : Profile
    {
        public MappingModels()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
        }
    }
}
