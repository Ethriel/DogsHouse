using AutoMapper;
using DogsHouse.Database;
using DogsHouse.Database.Model;
using DogsHouse.Services;
using DogsHouse.Services.Abstraction;
using DogsHouse.Services.DataPresentation;
using DogsHouse.Services.Model;
using DogsHouse.Services.Model.Mapper;
using DogsHouse.Services.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace DogsHouse.Tests.Utility
{
    public static class MockUtility
    {
        public static IEntityService<Dog> MockDogEntityService()
        {
            var dbContext = GetMockedDbContext(TestDataUtility.GetTestDogs(), context => context.Dogs);
            return BuildDogEntityService(dbContext);
        }

        public static IEntityService<Dog> MockDogEntityServiceEmptyDb()
        {
            var dbContext = GetMockedDbContext(new List<Dog>(), context => context.Dogs);
            return BuildDogEntityService(dbContext);
        }

        public static IEntityExtendedService<Dog> MockDogEntityExtendedService()
        {
            var dbContext = GetMockedDbContext(TestDataUtility.GetTestDogs(), context => context.Dogs);
            return BuildDogExtendedService(dbContext);
        }

        public static IEntityExtendedService<Dog> MockDogEntityExtendedServiceEmptyDb()
        {
            var dbContext = GetMockedDbContext(new List<Dog>(), context => context.Dogs);
            return BuildDogExtendedService(dbContext);
        }

        public static IMapperService<Dog, DogDTO> MockMapperService()
        {
            var serviceProvider = GetServiceProvider();
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            return new Mock<MapperService<Dog, DogDTO>>(mapper).Object;
        }

        public static IDogService MockDogService()
        {
            var dbContext = GetMockedDbContext(TestDataUtility.GetTestDogs(), context => context.Dogs);

            return BuildDogService(dbContext);
        }

        public static IDogService MockDogServiceEmptyDb()
        {
            var dbContext = GetMockedDbContext(new List<Dog>(), context => context.Dogs);

            return BuildDogService(dbContext);
        }

        private static DbContext GetMockedDbContext(List<Dog> inputData,
            Expression<Func<DogsHouseContext, DbSet<Dog>>> dbSetSelectionExpression)
        {
            var inputDataQueryable = inputData.AsQueryable();
            var dbSetMock = new Mock<DbSet<Dog>>();
            var dbMockContext = new Mock<DogsHouseContext>();

            dbSetMock.As<IQueryable<Dog>>().Setup(s => s.Provider).Returns(inputDataQueryable.Provider);
            dbSetMock.As<IQueryable<Dog>>().Setup(s => s.Expression).Returns(inputDataQueryable.Expression);
            dbSetMock.As<IQueryable<Dog>>().Setup(s => s.ElementType).Returns(inputDataQueryable.ElementType);
            dbSetMock.As<IQueryable<Dog>>().Setup(s => s.GetEnumerator()).Returns(() => inputDataQueryable.GetEnumerator());

            dbSetMock.Setup(set => set.Find(It.IsAny<object[]>())).Returns<object[]>(id =>
                inputData.FirstOrDefault(dog => dog.Id == (int)id.First()));

            dbSetMock.Setup(set => set.AsQueryable()).Returns(inputDataQueryable);
            dbSetMock.Setup(set => set.Add(It.IsAny<Dog>())).Callback<Dog>(inputData.Add);
            dbSetMock.Setup(set => set.AddRange(It.IsAny<IEnumerable<Dog>>())).Callback<IEnumerable<Dog>>(inputData.AddRange);
            dbSetMock.Setup(set => set.Remove(It.IsAny<Dog>())).Callback<Dog>(record => inputData.Remove(record));

            dbSetMock.Setup(set => set.RemoveRange(It.IsAny<IEnumerable<Dog>>())).Callback<IEnumerable<Dog>>(data =>
            {
                foreach (var record in data) { inputData.Remove(record); }
            });

            dbMockContext.Setup(dbSetSelectionExpression).Returns(dbSetMock.Object);
            dbMockContext.Setup(d => d.Set<Dog>()).Returns(dbSetMock.Object);
            dbMockContext.Setup(d => d.Model).Returns(MockContextModel().Object);
            dbMockContext.Setup(d => d.SaveChanges()).Returns(1);

            return dbMockContext.Object;
        }

        private static Mock<IModel> MockContextModel()
        {
            var mockModel = new Mock<IModel>();

            var mockDogEntityType = new Mock<IEntityType>();
            mockDogEntityType.Setup(m => m.ClrType).Returns(typeof(Dog));

            mockModel.Setup(m => m.GetEntityTypes()).Returns(new List<IEntityType> { mockDogEntityType.Object });

            return mockModel;
        }

        private static IEntityService<Dog> BuildDogEntityService(DbContext dbContext)
        {
            return new Mock<EntityService<Dog>>(dbContext).Object;
        }

        private static IEntityExtendedService<Dog> BuildDogExtendedService(DbContext dbContext)
        {
            return new Mock<EntityExtendedService<Dog>>(dbContext).Object;
        }

        private static IDogService BuildDogService(DbContext dbContext)
        {
            var entityExtendedService = BuildDogExtendedService(dbContext);

            var mapperService = MockMapperService();
            var serviceProvider = GetServiceProvider();
            var dogsValidator = serviceProvider.GetRequiredService<IValidator<DogDTO>>();
            var dogsSortingValidator = serviceProvider.GetRequiredService<IValidator<DogsSortingFilter>>();
            var DogsPagingValidator = serviceProvider.GetRequiredService<IValidator<DogsPagination>>();
            var logger = serviceProvider.GetRequiredService<ILogger<IDogService>>();

            return new Mock<DogService>(entityExtendedService, mapperService, dogsValidator, dogsSortingValidator, DogsPagingValidator, logger).Object;
        }

        private static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddAutoMapper(typeof(DogMapperProfile))
                             .AddLogging()
                             .AddScoped<IValidator<DogDTO>, DogValidator>()
                             .AddScoped<IValidator<DogsSortingFilter>, DogsSortingValidator>()
                             .AddScoped<IValidator<DogsPagination>, DogsPagingValidator>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
