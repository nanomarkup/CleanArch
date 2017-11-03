using Core.Entities;
using Core.Models;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace TestEntities
{
    public class UserEntityTests : IClassFixture<UserFixture>
    {
        readonly UserFixture fixture;

        public UserEntityTests(UserFixture fixture)
        {
            this.fixture = fixture;
        }

        IUserEntity<IUserModel> CreateEntity()
        {
            return fixture.Provider.GetService<IUserEntity<IUserModel>>();
        }

        IUserModel CreateModel()
        {
            var created = DateTime.Now;
            return new UserModel()
            {
                Id = Guid.NewGuid(),
                Created = created,
                Modified = created,
                FirstName = "User name",
                LastName = "User last name",
                Email = "User email"
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
            Assert.Equal(model.FirstName, entity.Attrs.FirstName);
            Assert.Equal(model.LastName, entity.Attrs.LastName);
            Assert.Equal(model.Email, entity.Attrs.Email);
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
            Assert.Equal(model.FirstName, entity.Attrs.FirstName);
            Assert.Equal(model.LastName, entity.Attrs.LastName);
            Assert.Equal(model.Email, entity.Attrs.Email);
        }

        [Theory]
        [InlineData("FirstName")]
        [InlineData("LastName")]
        [InlineData("Email")]
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

    public class UserFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public UserFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IUserEntity<IUserModel>, UserEntity>();
            Provider = services.BuildServiceProvider();
        }

        public void Dispose() { }
    }
}
