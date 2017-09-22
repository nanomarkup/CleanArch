using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Core.Entities;
using Entities;

namespace TestEntities
{   
    public class IdentityEntityTests : IClassFixture<IdentityFixture>
    {
        readonly IdentityFixture fixture;

        public IdentityEntityTests(IdentityFixture fixture)
        {
            this.fixture = fixture;
        }

        IIdentityEntity CreateIdentityEntity()
        {
            return fixture.Provider.GetService<IIdentityEntity>();
        }

        DtoEIdentity CreateDtoIdentityEntity()
        {
            return new DtoEIdentity()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Modified = DateTime.Now
            };
        }

        [Fact]
        public void TestEmptyObject()
        {
            var identity = CreateIdentityEntity();
            Assert.Equal(default(Guid), identity.Id);
            Assert.Equal(default(DateTime), identity.Created);
            Assert.Equal(default(DateTime), identity.Modified);
        }

        [Fact]
        public void TestCreation()
        {
            var identity = CreateIdentityEntity();
            identity.Create();
            Assert.NotNull(identity.Id);
            Assert.False(Guid.Empty == identity.Id);
            Assert.Equal(DateTime.Now.ToShortDateString(), identity.Created.ToShortDateString());
            Assert.Equal(DateTime.Now.ToShortDateString(), identity.Modified.ToShortDateString());
        }

        [Fact]
        public void TestInitialization()
        {
            var identity = CreateIdentityEntity();
            var dtoIdentity = CreateDtoIdentityEntity();
            identity.Initialize(dtoIdentity);
            Assert.True(dtoIdentity.Id == identity.Id);
            Assert.Equal(dtoIdentity.Created, identity.Created);
            Assert.Equal(dtoIdentity.Modified, identity.Modified);
        }

        [Fact]
        public void TestIsModified()
        {
            var identity = CreateIdentityEntity();
            identity.Initialize(CreateDtoIdentityEntity());

            Assert.False(identity.IsModified());
            identity.BeginUpdate();
            Assert.False(identity.IsModified());
            identity.EndUpdate();
            Assert.False(identity.IsModified());
        }

        [Fact]
        public void TestChangedEvent()
        {
            var identity = CreateIdentityEntity();
            var isPropertyChanged = false;
            identity.Create();
            identity.Changed += (sender, args) => { isPropertyChanged = true; };

            identity.BeginUpdate();
            Assert.False(identity.IsModified());
            identity.EndUpdate();
            // EndUpdate method will not perform Changed event if object is not changed            
            // It is impossible to change the data object because all properties are read only
            // Should be False because the data object is not changed
            Assert.False(isPropertyChanged);
        }
    }

    public class IdentityFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public IdentityFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IIdentityEntity, IdentityEntity>();
            Provider = services.BuildServiceProvider();
        }

        public void Dispose() { }
    }
}
