using AutoMapper;
using AutoMapper.Configuration;
using Core.Entities;
using Core.Interactors;
using Entities;
using Interactors;
using Loader;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using Xunit;

namespace TestServices
{
    public class ServiceFixture : IDisposable
    {
        public ServiceFixture()
        {
            MapperInitializer.Initialize();
            DependencyInitializer.Initialize();
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
            Service.Message.Send.Invoke(new DtoServiceMessageSend()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "Test Message"
            });            
        }
    }
}
