using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Core.Common;
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
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new EntityProfile());
                cfg.AddProfile(new GatewayProfile());
                cfg.AddProfile(new InteractorProfile());
            });

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
