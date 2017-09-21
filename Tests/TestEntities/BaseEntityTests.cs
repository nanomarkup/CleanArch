using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Core.Entities;
using Entities;

namespace TestEntities
{
    public class BaseEntityTests : IClassFixture<BaseFixture>
    {
        readonly BaseFixture fixture;

        public BaseEntityTests(BaseFixture fixture)
        {
            this.fixture = fixture;
        }

        IBaseEntity CreateIdentityEntity()
        {
            return fixture.Provider.GetService<IBaseEntity>();
        }

        [Fact]
        public void TestIsModified()
        {
            var entity = CreateIdentityEntity();
            Assert.False(entity.IsModified());
            entity.BeginUpdate();
            Assert.False(entity.IsModified());
            entity.EndUpdate();
            Assert.False(entity.IsModified());
        }

        [Fact]
        public void TestChangedEvent()
        {
            var data = CreateIdentityEntity();
            var isPropertyChanged = false;
            data.Changed += (sender, args) => { isPropertyChanged = true; };
            data.BeginUpdate();
            Assert.False(data.IsModified());
            data.EndUpdate();
            // EndUpdate method will not perform PropertyChanged event if object is not changed            
            // It is impossible to change the data object because all properties are read only
            // Should be False because the data object is not changed
            Assert.False(isPropertyChanged);
        }
    }

    public class BaseFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public BaseFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IBaseEntity, BaseEntity>();
            Provider = services.BuildServiceProvider();
        }

        public void Dispose() { }
    }
}
