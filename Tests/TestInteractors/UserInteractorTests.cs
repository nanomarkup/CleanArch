using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using AutoMapper;
using Core.Entities;
using Core.Interactors;
using Core.Gateways;
using Core.AutoMapper;
using Entities;
using Interactors;
using Infrastructure.Gateways;

namespace TestInteractors
{
    public class UserFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public UserFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IUserEntity, UserEntity>();
            services.AddTransient<IIdentityEntity, IdentityEntity>();
            services.AddTransient<IUserInteractor, UserInteractor>();
            services.AddTransient<IUserGateway, UserGateway>();
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

    public class UserInteractorTests : IClassFixture<UserFixture>
    {
        readonly UserFixture fixture;

        public UserInteractorTests(UserFixture fixture)
        {
            this.fixture = fixture;
        }

        IUserInteractor CreateUserInteractor()
        {
            return fixture.Provider.GetService<IUserInteractor>();
        }

        [Theory]
        [InlineData("First name", "Last name", "email@mail.com")]
        [InlineData("", "Last name", "email@mail.com")]
        [InlineData("First name", "", "email@mail.com")]
        [InlineData("First name", "Last name", "")]
        public void TestCreate(string firstName, string lastName, string email)
        {
            var user = CreateUserInteractor();
            if ((new List<string>() { firstName, lastName, email }).All(x => x.Length > 0))
            {
                var userId = user.Create(new DtoIUserCreate()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                });
                Assert.NotNull(userId);
                Assert.False(userId == Guid.Empty);
            }
            else
            {
                var ex = Assert.Throws<ArgumentException>(() => user.Create(new DtoIUserCreate()
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
