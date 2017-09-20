using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Core.Entities;
using Entities;

namespace TestEntities
{
    public class DataFixture : IDisposable
    {
        public IServiceProvider Provider { get; }

        public DataFixture()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IDataEntity, DataEntity>();
            Provider = serviceCollection.BuildServiceProvider();
        }

        public void Dispose() { }
    }

    public class DataEntityTests : IClassFixture<DataFixture>
    {
        readonly DataFixture fixture;

        public DataEntityTests(DataFixture fixture)
        {
            this.fixture = fixture;
        }

        IDataEntity CreateDataEntity()
        {
            return fixture.Provider.GetService<IDataEntity>();
        }

        [Fact]
        public void TestEmptyObject()
        {
            var data = CreateDataEntity();
            Assert.True(data.Id == null);
            Assert.Equal(default(DateTime), data.Created);
            Assert.Equal(default(DateTime), data.Modified);
        }

        [Fact]
        public void TestCreation()
        {
            var data = CreateDataEntity();            
            data.Create();
            Assert.False(data.Id == null);
            Assert.False(data.Id == Guid.Empty);
            Assert.Equal(DateTime.Now.ToShortDateString(), data.Created.ToShortDateString());
            Assert.Equal(DateTime.Now.ToShortDateString(), data.Modified.ToShortDateString());
        }

        [Fact]
        public void TestInitialization()
        {
            var data = CreateDataEntity();
            var dtoData = new DtoDataEntity() { Id = Guid.NewGuid(), Created = DateTime.Now, Modified = DateTime.Now };
            data.Initialize(dtoData);
            Assert.True(data.Id == dtoData.Id);
            Assert.Equal(dtoData.Created, data.Created);
            Assert.Equal(dtoData.Modified, data.Modified);
        }

        [Fact]
        public void TestPropertyChangedEvent()
        {
            var data = CreateDataEntity();
            var isPropertyChanged = false;
            data.Create();
            data.PropertyChanged += (sender, args) => { isPropertyChanged = true; };
            data.BeginUpdate();
            Assert.False(data.IsModified());
            data.EndUpdate();
            // EndUpdate method will not perform PropertyChanged event if object is not changed            
            // It is impossible to change the data object because all properties are read only
            // Should be False because the data object is not changed
            Assert.False(isPropertyChanged);
        }
    }
}
