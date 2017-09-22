using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Core.Entities;
using Entities;

namespace TestEntities
{
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

        DtoEUserIdentity CreateDtoUserIdentity()
        {
            return new DtoEUserIdentity()
            {
                FirstName = "User name",
                LastName = "User last name",
                Email = "User email",
                Identity = new DtoEIdentity()
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                }
            };
        }

        DtoEUser CreateDtoUserEntity()
        {
            return new DtoEUser()
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
            Assert.Equal(default(Guid), user.Identity.Id);
            Assert.Equal(default(DateTime), user.Identity.Created);
            Assert.Equal(default(DateTime), user.Identity.Modified);
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
            Assert.NotNull(user.Identity.Id);
            Assert.False(Guid.Empty == user.Identity.Id);
            Assert.Equal(DateTime.Now.ToShortDateString(), user.Identity.Created.ToShortDateString());
            Assert.Equal(DateTime.Now.ToShortDateString(), user.Identity.Modified.ToShortDateString());
            Assert.Equal(dtoUser.FirstName, user.FirstName);
            Assert.Equal(dtoUser.LastName, user.LastName);
            Assert.Equal(dtoUser.Email, user.Email);
        }

        [Fact]
        public void TestInitialization()
        {
            var user = CreateUserEntity();
            var dtoUser = CreateDtoUserIdentity();
            user.Initialize(dtoUser);
            Assert.True(dtoUser.Identity.Id == user.Identity.Id);
            Assert.Equal(dtoUser.Identity.Created, user.Identity.Created);
            Assert.Equal(dtoUser.Identity.Modified, user.Identity.Modified);
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
            var dtoUser = CreateDtoUserIdentity();
            var property = user.GetType().GetProperty(propertyName);
            user.Initialize(dtoUser);

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
        public void TestChangedEvent(string propertyName)
        {
            var user = CreateUserEntity();
            var dtoUser = CreateDtoUserEntity();
            var isPropertyChanged = false;
            user.Create(dtoUser);
            user.Changed += (sender, args) => { isPropertyChanged = true; };

            user.GetType().GetProperty(propertyName).SetValue(user, "New value");
            Assert.True(isPropertyChanged);
        }
    }

    public class UserFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public UserFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IUserEntity, UserEntity>();
            services.AddTransient<IIdentityEntity, IdentityEntity>();
            Provider = services.BuildServiceProvider();
        }

        public void Dispose() { }
    }
}
