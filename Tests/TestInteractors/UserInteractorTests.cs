using Loader;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestInteractors
{
    public class UserFixture : IDisposable
    { 
        public UserFixture()
        {
            MapperInitializer.Initialize();
            DependencyInitializer.Initialize();                 
        }

        public void Dispose() { }
    }

    public class UserInteractorTests : IClassFixture<UserFixture>
    {
        readonly UserFixture fixture;

        public UserInteractorTests(UserFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("First name", "Last name", "email@mail.com")]
        [InlineData("", "Last name", "email@mail.com")]
        [InlineData("First name", "", "email@mail.com")]
        [InlineData("First name", "Last name", "")]
        public void TestCreate(string firstName, string lastName, string email)
        {            
            if ((new List<string>() { firstName, lastName, email }).All(x => x.Length > 0))
            {
                var userId = Service.User.Create.Invoke(new DtoServiceUserCreate()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                });
                Assert.NotNull(userId);
                Assert.False(userId.Id == Guid.Empty);
            }
            else
            {
                var ex = Assert.Throws<ArgumentException>(() => Service.User.Create.Invoke(new DtoServiceUserCreate()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                }));
                Assert.True(ex.Message.Contains(" is empty."));
            }            
        }
    }
}
