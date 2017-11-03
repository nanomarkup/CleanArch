using Core.Entities;
using Core.Gateways;
using Core.Interactors;
using Core.Models;
using Entities;
using Infrastructure.Context;
using Infrastructure.Gateways;
using Interactors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;

namespace Loader
{
    public static class DependencyInitializer
    {
        public static void Initialize()
        {
            IServiceCollection service = new ServiceCollection();
            service.UseEntities();
            service.UseInteractors();
            service.UseInfrastructure();
            service.AddDbContext<InfrastructureDbContext>(opt => opt.UseInMemoryDatabase("example"), ServiceLifetime.Singleton, ServiceLifetime.Singleton);
            Service.Provider = service.BuildServiceProvider();
        }

        public static void UseEntities(this IServiceCollection service)
        {
            service.AddTransient<IMessageEntity<IMessageModel>, MessageEntity>();
            service.AddTransient<IUserEntity<IUserModel>, UserEntity>();
        }

        public static void UseEntities(this IServiceCollection service, IEnumerable<Type> interfaces)
        {
            foreach (var type in interfaces)
            {
                if (type == typeof(IMessageEntity<IMessageModel>))
                    service.AddTransient<IMessageEntity<IMessageModel>, MessageEntity>();
                else if (type == typeof(IUserEntity<IUserModel>))
                    service.AddTransient<IUserEntity<IUserModel>, UserEntity>();
            }
        }

        public static void UseInteractors(this IServiceCollection service)
        {
            service.AddTransient<IMessageInteractor, MessageInteractor>();
            service.AddTransient<IUserInteractor, UserInteractor>();
        }

        public static void UseInteractors(this IServiceCollection service, IEnumerable<Type> interfaces)
        {
            foreach (var type in interfaces)
            {
                if (type == typeof(IMessageInteractor))
                    service.AddTransient<IMessageInteractor, MessageInteractor>();
                else if (type == typeof(IUserInteractor))
                    service.AddTransient<IUserInteractor, UserInteractor>();
            }
        }

        public static void UseContext(this IServiceCollection service)
        {
            service.AddTransient<IInfrastructureDbContext, InfrastructureDbContext>();
        }

        public static void UseGateways(this IServiceCollection service)
        {
            service.AddTransient<IMessageGateway, MessageGateway>();
            service.AddTransient<IUserGateway, UserGateway>();
        }

        public static void UseGateways(this IServiceCollection service, IEnumerable<Type> interfaces)
        {
            foreach (var type in interfaces)
            {
                if (type == typeof(IMessageGateway))
                    service.AddTransient<IMessageGateway, MessageGateway>();
                else if (type == typeof(IUserGateway))
                    service.AddTransient<IUserGateway, UserGateway>();
            }
        }

        public static void UseInfrastructure(this IServiceCollection service)
        {
            UseContext(service);
            UseGateways(service);
        }
    }
}
