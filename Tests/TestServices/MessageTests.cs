using System;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using AutoMapper.Configuration;
using Xunit;
using Core.Mapping;
using Core.Entities;
using Core.Interactors;
using Entities;
using Interactors;
using Loader;
using Services;

namespace TestServices
{
    public class ServiceFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public ServiceFixture()
        {
            var configuration = new MapperConfigurationExpression();
            configuration.UseCore();
            Mapper.Initialize(configuration);

            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IMessageEntity, MessageEntity>();
            services.AddTransient<IIdentityEntity, IdentityEntity>();
            services.AddTransient<IMessageInteractor, MessageInteractor>();
            Provider = services.BuildServiceProvider();            
        }

        public void Dispose() { }
    }

    public class MessageTests : IClassFixture<ServiceFixture>
    {
        readonly ServiceFixture fixture;

        public MessageTests(ServiceFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void TestSendMessage()
        {
            Service.Provider = fixture.Provider;
            Service.Message.Send.Invoke(new DtoMessageSendInteractor()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "Test Message"
            });            
        }
    }
}
