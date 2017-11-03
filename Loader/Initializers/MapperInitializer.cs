﻿using AutoMapper;
using AutoMapper.Configuration;
using Core.Mapping;
using Infrastructure.Mapping;
using Services.Mapping;

namespace Loader
{
    public static class MapperInitializer
    {        
        public static void Initialize()
        {
            var configuration = new MapperConfigurationExpression();
            configuration.UseCore();
            configuration.UseServices();
            Mapper.Initialize(configuration);
        }

        public static void UseCore(this MapperConfigurationExpression configuration)
        {
            UseModels(configuration);
            UseEntities(configuration);
            UseGateways(configuration);
            UseInteractors(configuration);
        }

        public static void UseModels(this MapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new ModelsProfile());
        }

        public static void UseEntities(this MapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new EntitiesProfile());
        }

        public static void UseGateways(this MapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new GatewaysProfile());
        }

        public static void UseInteractors(this MapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new InteractorsProfile());
        }    
        
        public static void UseServices(this MapperConfigurationExpression configuration)
        {
            configuration.AddProfile(new ServicesProfile());
        }
    }
}
