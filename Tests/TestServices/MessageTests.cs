using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using AutoMapper;
using Core.Mapper;
using Core.Entities;
using Core.Interactors;
using Entities;
using Interactors;
using Services;

namespace TestServices
{
    public class ServiceFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public ServiceFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IMessageEntity, MessageEntity>();
            services.AddTransient<IIdentityEntity, IdentityEntity>();
            services.AddTransient<IMessageInteractor, MessageInteractor>();
            Provider = services.BuildServiceProvider();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new EntityProfile());
                cfg.AddProfile(new GatewayProfile());
                cfg.AddProfile(new InteractorProfile());
            });
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
            Service.Message.Send.Invoke(new DtoIMessageSend()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "Test Message"
            });            
        }
    }
}
