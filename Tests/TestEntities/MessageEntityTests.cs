using Core.Entities;
using Core.Models;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace TestEntities
{
    public class MessageEntityTests : IClassFixture<MessageFixture>
    {
        readonly MessageFixture fixture;

        public MessageEntityTests(MessageFixture fixture)
        {
            this.fixture = fixture;
        }

        IMessageEntity<IMessageModel> CreateEntity()
        {
            return fixture.Provider.GetService<IMessageEntity<IMessageModel>>();
        }        

        IMessageModel CreateModel()
        {
            var created = DateTime.Now;
            return new MessageModel()
            {
                Id = Guid.NewGuid(),
                Created = created,
                Modified = created,
                Sender = Guid.NewGuid(),
                Receiver = Guid.NewGuid(),
                Text = "Message"
            };
        }

        [Fact]
        public void TestEmptyObject()
        {
            var entity = CreateEntity();
            Assert.Null(entity.Attrs);
        }

        [Fact]
        public void TestCreation()
        {
            var model = CreateModel();
            var entity = CreateEntity();
            entity.Create(model);
            Assert.NotNull(entity.Attrs.Id);
            Assert.False(entity.Attrs.Id == Guid.Empty);
            Assert.True(model.Id == entity.Attrs.Id);
            Assert.Equal(model.Created, entity.Attrs.Created);
            Assert.Equal(model.Modified, entity.Attrs.Modified);
            Assert.Equal(model.Sender, entity.Attrs.Sender);
            Assert.Equal(model.Receiver, entity.Attrs.Receiver);
            Assert.Equal(model.Text, entity.Attrs.Text);
        }

        [Fact]
        public void TestInitialization()
        {
            var model = CreateModel();
            var entity = CreateEntity();
            entity.Initialize(model);
            Assert.NotNull(entity.Attrs.Id);
            Assert.False(entity.Attrs.Id == Guid.Empty);
            Assert.True(model.Id == entity.Attrs.Id);
            Assert.Equal(model.Created, entity.Attrs.Created);
            Assert.Equal(model.Modified, entity.Attrs.Modified);
            Assert.Equal(model.Sender, entity.Attrs.Sender);
            Assert.Equal(model.Receiver, entity.Attrs.Receiver);
            Assert.Equal(model.Text, entity.Attrs.Text);
        }

        [Theory]
        [InlineData("Text")]
        public void TestChangedEvent(string propertyName)
        {
            var entity = CreateEntity();
            entity.Create(CreateModel());

            var isPropertyChanged = false;
            entity.Changed += (sender, args) => { isPropertyChanged = true; };
            entity.Attrs.GetType().GetProperty(propertyName).SetValue(entity.Attrs, "New value");
            Assert.True(isPropertyChanged);
        }
    }

    public class MessageFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public MessageFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IMessageEntity<IMessageModel>, MessageEntity>();
            Provider = services.BuildServiceProvider();
        }

        public void Dispose() { }
    }
}
