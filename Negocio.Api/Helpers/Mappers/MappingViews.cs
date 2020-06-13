using AutoMapper;
using Data.Api.Models;
using Domain.Api.ViewModels.Login;

namespace Logic.Api.Helpers.Mappers
{
    public class MappingViews : Profile
    {
        public MappingViews()
        {
            CreateMap<LogLoginViewModel, LoginLogModel>();
        }
    }
}
