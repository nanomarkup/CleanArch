using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Core.Entities;
using Entities;

namespace TestEntities
{
    public class MessageFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public MessageFixture()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IMessageEntity, MessageEntity>();
            Provider = serviceCollection.BuildServiceProvider();
        }

        public void Dispose() { }
    }

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

        DtoDataEntity CreateDtoDataEntity()
        {
            return new DtoDataEntity()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Modified = DateTime.Now
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
            Assert.Equal(default(Guid?), message.Id);
            Assert.Equal(default(DateTime), message.Created);
            Assert.Equal(default(DateTime), message.Modified);
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
            Assert.False(message.Id == null);
            Assert.False(message.Id == Guid.Empty);
            Assert.Equal(DateTime.Now.ToShortDateString(), message.Created.ToShortDateString());
            Assert.Equal(DateTime.Now.ToShortDateString(), message.Modified.ToShortDateString());
            Assert.Equal(dtoMessage.Sender, message.Sender);
            Assert.Equal(dtoMessage.Receiver, message.Receiver);
            Assert.Equal(dtoMessage.Text, message.Text);
        }

        [Fact]
        public void TestInitialization()
        {
            var message = CreateMessageEntity();
            var dtoData = CreateDtoDataEntity();
            var dtoMessage = CreateDtoMessageEntity();
            message.Initialize(dtoData, dtoMessage);
            Assert.True(message.Id == dtoData.Id);
            Assert.Equal(dtoData.Created, message.Created);
            Assert.Equal(dtoData.Modified, message.Modified);
            Assert.Equal(dtoMessage.Sender, message.Sender);
            Assert.Equal(dtoMessage.Receiver, message.Receiver);
            Assert.Equal(dtoMessage.Text, message.Text);
        }

        [Theory]
        [InlineData("Text")]
        public void TestIsModified(string propertyName)
        {
            var message = CreateMessageEntity();
            var dtoData = CreateDtoDataEntity();
            var dtoMessage = CreateDtoMessageEntity();
            var property = message.GetType().GetProperty(propertyName);
            message.Initialize(dtoData, dtoMessage);

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
        public void TestPropertyChanged(string propertyName)
        {
            var message = CreateMessageEntity();
            var isPropertyChanged = false;
            message.Create();
            message.PropertyChanged += (sender, args) => { isPropertyChanged = true; };

            message.GetType().GetProperty(propertyName).SetValue(message, "New value");
            Assert.True(isPropertyChanged);
        }

        [Fact]
        public void TestSend()
        {
            var message = CreateMessageEntity();
            var isPropertyChanged = false;
            message.Create();
            message.PropertyChanged += (sender, args) => { isPropertyChanged = true; };

            message.Send();            
            Assert.True(isPropertyChanged);
        }
    }
}
