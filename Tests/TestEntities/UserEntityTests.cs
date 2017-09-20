using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Core.Entities;
using Entities;

namespace TestEntities
{
    public class UserFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public UserFixture()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IUserEntity, UserEntity>();
            Provider = serviceCollection.BuildServiceProvider();
        }

        public void Dispose() { }
    }

    public class UserEntityTests : IClassFixture<UserFixture>
    {
        readonly UserFixture fixture;

        public UserEntityTests(UserFixture fixture)
        {
            this.fixture = fixture;
        }

        IUserEntity CreateUserEntity()
        {
            return fixture.Provider.GetService<IUserEntity>();
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

        DtoUserEntity CreateDtoUserEntity()
        {
            return new DtoUserEntity()
            {
                FirstName = "User name",
                LastName = "User last name",
                Email = "User email"
            };
        }

        [Fact]
        public void TestEmptyObject()
        {
            var user = CreateUserEntity();
            Assert.Equal(default(Guid?), user.Id);
            Assert.Equal(default(DateTime), user.Created);
            Assert.Equal(default(DateTime), user.Modified);
            Assert.Equal(default(string), user.FirstName);
            Assert.Equal(default(string), user.LastName);
            Assert.Equal(default(string), user.Email);
        }

        [Fact]
        public void TestCreation()
        {
            var user = CreateUserEntity();
            var dtoUser = CreateDtoUserEntity();
            user.Create(dtoUser);
            Assert.False(user.Id == null);
            Assert.False(user.Id == Guid.Empty);
            Assert.Equal(DateTime.Now.ToShortDateString(), user.Created.ToShortDateString());
            Assert.Equal(DateTime.Now.ToShortDateString(), user.Modified.ToShortDateString());
            Assert.Equal(dtoUser.FirstName, user.FirstName);
            Assert.Equal(dtoUser.LastName, user.LastName);
            Assert.Equal(dtoUser.Email, user.Email);
        }

        [Fact]
        public void TestInitialization()
        {
            var user = CreateUserEntity();
            var dtoData = CreateDtoDataEntity();
            var dtoUser = CreateDtoUserEntity();
            user.Initialize(dtoData, dtoUser);
            Assert.True(user.Id == dtoData.Id);
            Assert.Equal(dtoData.Created, user.Created);
            Assert.Equal(dtoData.Modified, user.Modified);
            Assert.Equal(dtoUser.FirstName, user.FirstName);
            Assert.Equal(dtoUser.LastName, user.LastName);
            Assert.Equal(dtoUser.Email, user.Email);
        }

        [Theory]
        [InlineData("FirstName")]
        [InlineData("LastName")]
        [InlineData("Email")]
        public void TestIsModified(string propertyName)
        {
            var user = CreateUserEntity();
            var dtoData = CreateDtoDataEntity();
            var dtoUser = CreateDtoUserEntity();
            var property = user.GetType().GetProperty(propertyName);
            user.Initialize(dtoData, dtoUser);

            Assert.False(user.IsModified());
            property.SetValue(user, "New value 1");
            Assert.False(user.IsModified());

            user.BeginUpdate();
            Assert.False(user.IsModified());
            property.SetValue(user, "New value 2");
            Assert.True(user.IsModified());
            user.EndUpdate();
            Assert.False(user.IsModified());
        }

        [Theory]
        [InlineData("FirstName")]
        [InlineData("LastName")]
        [InlineData("Email")]
        public void TestPropertyChanged(string propertyName)
        {
            var user = CreateUserEntity();
            var isPropertyChanged = false;
            user.Create();
            user.PropertyChanged += (sender, args) => { isPropertyChanged = true; };

            user.GetType().GetProperty(propertyName).SetValue(user, "New value");
            Assert.True(isPropertyChanged);
        }
    }
}
