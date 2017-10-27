using AutoMapper;
using AutoMapper.Configuration;
using Core.Mapping;
using Infrastructure.Mapping;

namespace Loader
{
    public static class MapperInitializer
    {        
        public static void Initialize()
        {
            var configuration = new MapperConfigurationExpression();
            configuration.UseCore();
            configuration.UseInfrastructure();
            Mapper.Initialize(configuration);
        }

        public static void UseCore(this MapperConfigurationExpression configuration)
        {
            UseEntities(configuration);
            UseGateways(configuration);
            UseInteractors(configuration);
        }

        public static void UseInfrastructure(this MapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new ModelsProfile());
            configuration.AddProfile(new Infrastructure.Mapping.GatewaysProfile());
        }

        public static void UseEntities(this MapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new EntitiesProfile());
        }

        public static void UseGateways(this MapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new Core.Mapping.GatewaysProfile());
        }

        public static void UseInteractors(this MapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new InteractorsProfile());
        }        
    }
}
