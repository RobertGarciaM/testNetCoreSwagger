using AutoMapper;

namespace Logic.Api.Helpers.Mappers
{
    public class MapperConfiguration : Profile
    {
        public static void Configuration()
        {
            Mapper.Initialize(cfg => { cfg.AddProfile<MappingModels>(); cfg.AddProfile<MappingViews>(); });
        }
    }
}
