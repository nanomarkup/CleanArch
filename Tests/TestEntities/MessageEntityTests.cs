using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Core.Entities;
using Entities;

namespace TestEntities
{   
    public class MessageEntityTests : IClassFixture<MessageFixture>
    {
        readonly MessageFixture fixture;

        public MessageEntityTests(MessageFixture fixture)
        {
            this.fixture = fixture;
        }

        IMessageEntity CreateMessageEntity()
        {
            return fixture.Provider.GetService<IMessageEntity>();
        }

        DtoMessageIdentity CreateDtoMessageIdentity()
        {
            return new DtoMessageIdentity()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "Message",
                Identity = new DtoIdentityEntity()
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                }
            };
        }

        DtoMessageEntity CreateDtoMessageEntity()
        {
            return new DtoMessageEntity()
            {
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "Message"
            };
        }

        [Fact]
        public void TestEmptyObject()
        {
            var message = CreateMessageEntity();
            Assert.Equal(default(Guid), message.Identity.Id);
            Assert.Equal(default(DateTime), message.Identity.Created);
            Assert.Equal(default(DateTime), message.Identity.Modified);
            Assert.Equal(default(Guid), message.Sender);
            Assert.Equal(default(Guid), message.Receiver);
            Assert.Equal(default(string), message.Text);
        }

        [Fact]
        public void TestCreation()
        {
            var message = CreateMessageEntity();
            var dtoMessage = CreateDtoMessageEntity();
            message.Create(dtoMessage);
            Assert.NotNull(message.Identity.Id);
            Assert.False(message.Identity.Id == Guid.Empty);
            Assert.Equal(DateTime.Now.ToShortDateString(), message.Identity.Created.ToShortDateString());
            Assert.Equal(DateTime.Now.ToShortDateString(), message.Identity.Modified.ToShortDateString());
            Assert.Equal(dtoMessage.Sender, message.Sender);
            Assert.Equal(dtoMessage.Receiver, message.Receiver);
            Assert.Equal(dtoMessage.Text, message.Text);
        }

        [Fact]
        public void TestInitialization()
        {
            var message = CreateMessageEntity();
            var dtoMessage = CreateDtoMessageIdentity();
            message.Initialize(dtoMessage);
            Assert.True(dtoMessage.Identity.Id == message.Identity.Id);
            Assert.Equal(dtoMessage.Identity.Created, message.Identity.Created);
            Assert.Equal(dtoMessage.Identity.Modified, message.Identity.Modified);
            Assert.Equal(dtoMessage.Sender, message.Sender);
            Assert.Equal(dtoMessage.Receiver, message.Receiver);
            Assert.Equal(dtoMessage.Text, message.Text);
        }

        [Theory]
        [InlineData("Text")]
        public void TestIsModified(string propertyName)
        {
            var message = CreateMessageEntity();
            var dtoMessage = CreateDtoMessageIdentity();
            var property = message.GetType().GetProperty(propertyName);
            message.Initialize(dtoMessage);

            Assert.False(message.IsModified());
            property.SetValue(message, "New value 1");
            Assert.False(message.IsModified());

            message.BeginUpdate();
            Assert.False(message.IsModified());
            property.SetValue(message, "New value 2");
            Assert.True(message.IsModified());
            message.EndUpdate();
            Assert.False(message.IsModified());
        }

        [Theory]
        [InlineData("Text")]
        public void TestChangedEvent(string propertyName)
        {
            var message = CreateMessageEntity();
            var dtoMessage = CreateDtoMessageEntity();
            var isPropertyChanged = false;            
            message.Create(dtoMessage);
            message.Changed += (sender, args) => { isPropertyChanged = true; };

            message.GetType().GetProperty(propertyName).SetValue(message, "New value");
            Assert.True(isPropertyChanged);
        }

        [Fact]
        public void TestSend()
        {
            var message = CreateMessageEntity();
            var dtoMessage = CreateDtoMessageEntity();
            var isPropertyChanged = false;
            message.Create(dtoMessage);
            message.Changed += (sender, args) => { isPropertyChanged = true; };

            message.Send();            
            Assert.True(isPropertyChanged);
        }
    }

    public class MessageFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public MessageFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IMessageEntity, MessageEntity>();
            services.AddTransient<IIdentityEntity, IdentityEntity>();
            Provider = services.BuildServiceProvider();
        }

        public void Dispose() { }
    }
}
