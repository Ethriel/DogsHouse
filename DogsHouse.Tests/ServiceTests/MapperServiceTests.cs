using DogsHouse.Database.Model;
using DogsHouse.Services.Abstraction;
using DogsHouse.Services.Model;
using DogsHouse.Tests.Utility;

namespace DogsHouse.Tests.ServiceTests
{
    public class MapperServiceTests
    {
        private readonly IMapperService<Dog, DogDTO> _mapperService;

        public MapperServiceTests()
        {
            _mapperService = MockUtility.MockMapperService();
        }

        #region MapDto
        [Fact]
        public void MapDto_MappsEntityToDTO()
        {
            var dog = TestDataUtility.GetTestDog();

            var dogDto = _mapperService.MapDto(dog);

            Assert.NotNull(dogDto);
            Assert.True(dog.Id == dogDto.Id && dog.Name == dogDto.Name);
        }

        [Fact]
        public async Task MapDtoAsync_MappsEntityToDTO()
        {
            var dog = TestDataUtility.GetTestDog();

            var dogDto = await _mapperService.MapDtoAsync(dog);

            Assert.NotNull(dogDto);
            Assert.True(dog.Id == dogDto.Id && dog.Name == dogDto.Name);
        }

        [Fact]
        public void MapDto_MappsNull()
        {
            var dogDto = _mapperService.MapDto(null);

            Assert.Null(dogDto);
        }

        [Fact]
        public async Task MapDtoAsync_MappsNull()
        {
            var dogDto = await _mapperService.MapDtoAsync(null);

            Assert.Null(dogDto);
        }
        #endregion

        #region MapDtos
        [Fact]
        public void MapDtos_MappsEntitiesToDTOs()
        {
            var dogs = TestDataUtility.GetTestDogs();
            var dogDtos = _mapperService.MapDtos(dogs);

            Assert.NotNull(dogDtos);
            Assert.NotEmpty(dogDtos);
            Assert.True(dogs.FirstOrDefault()?.Id == dogDtos.FirstOrDefault()?.Id && dogs.First().Name == dogDtos.FirstOrDefault()?.Name);
        }

        [Fact]
        public async Task MapDtosAsync_MappsEntitiesToDTOs()
        {
            var dogs = TestDataUtility.GetTestDogs();
            var dogDtos = await _mapperService.MapDtosAsync(dogs);

            Assert.NotNull(dogDtos);
            Assert.NotEmpty(dogDtos);
            Assert.True(dogs.FirstOrDefault()?.Id == dogDtos.FirstOrDefault()?.Id && dogs.First().Name == dogDtos.FirstOrDefault()?.Name);
        }

        [Fact]
        public void MapDtos_MappsEmptyFromNull()
        {
            var dogDtos = _mapperService.MapDtos(null);

            Assert.NotNull(dogDtos);
            Assert.Empty(dogDtos);
        }

        [Fact]
        public async Task MapDtosAsync_MappsEmptyFromNull()
        {
            var dogDtos = await _mapperService.MapDtosAsync(null);

            Assert.NotNull(dogDtos);
            Assert.Empty(dogDtos);
        }

        [Fact]
        public void MapDtos_MappsEmptyFromEmpty()
        {
            var dogDtos = _mapperService.MapDtos(new List<Dog>());

            Assert.NotNull(dogDtos);
            Assert.Empty(dogDtos);
        }

        [Fact]
        public async Task MapDtosAsync_MappsEmptyFromEmpty()
        {
            var dogDtos = await _mapperService.MapDtosAsync(new List<Dog>());

            Assert.NotNull(dogDtos);
            Assert.Empty(dogDtos);
        }
        #endregion

        #region MapEntity
        [Fact]
        public void MapEntity_MappsEntityFromDTO()
        {
            var dogDto = TestDataUtility.GetTestDogDTO();

            var dog = _mapperService.MapEntity(dogDto);

            Assert.NotNull(dog);
            Assert.True(dog.Id == dogDto.Id && dog.Name == dogDto.Name);
        }

        [Fact]
        public async Task MapEntityAsync_MappsEntityFromDTO()
        {
            var dogDto = TestDataUtility.GetTestDogDTO();

            var dog = await _mapperService.MapEntityAsync(dogDto);

            Assert.NotNull(dog);
            Assert.True(dog.Id == dogDto.Id && dog.Name == dogDto.Name);
        }

        [Fact]
        public void MapEntity_MappsNull()
        {
            var dog = _mapperService.MapEntity(null);

            Assert.Null(dog);
        }

        [Fact]
        public async Task MapEntityAsync_MappsNull()
        {
            var dog = await _mapperService.MapEntityAsync(null);

            Assert.Null(dog);
        }
        #endregion

        #region MapEntities
        [Fact]
        public void MapEntities_MappsEntitiesFromDTOs()
        {
            var dogDtos = TestDataUtility.GetTestDogDTOs();
            var dogs = _mapperService.MapEntities(dogDtos);

            Assert.NotNull(dogDtos);
            Assert.NotEmpty(dogDtos);
            Assert.True(dogs.FirstOrDefault()?.Id == dogDtos.FirstOrDefault()?.Id && dogs.First().Name == dogDtos.FirstOrDefault()?.Name);
        }

        [Fact]
        public async Task MapEntitiesAsync_MappsEntitiesFromDTOs()
        {
            var dogDtos = TestDataUtility.GetTestDogDTOs();
            var dogs = await _mapperService.MapEntitiesAsync(dogDtos);

            Assert.NotNull(dogDtos);
            Assert.NotEmpty(dogDtos);
            Assert.True(dogs.FirstOrDefault()?.Id == dogDtos.FirstOrDefault()?.Id && dogs.First().Name == dogDtos.FirstOrDefault()?.Name);
        }

        [Fact]
        public void MapEntities_MappsEmptyFromNull()
        {
            var dogs = _mapperService.MapEntities(null);

            Assert.NotNull(dogs);
            Assert.Empty(dogs);
        }

        [Fact]
        public async Task MapEntitiesAsync_MappsEmptyFromNull()
        {
            var dogs = await _mapperService.MapEntitiesAsync(null);

            Assert.NotNull(dogs);
            Assert.Empty(dogs);
        }

        [Fact]
        public void MapEntities_MappsEmptyFromEmpty()
        {
            var dogs = _mapperService.MapEntities(new List<DogDTO>());

            Assert.NotNull(dogs);
            Assert.Empty(dogs);
        }

        [Fact]
        public async Task MapEntitiesAsync_MappsEmptyFromEmpty()
        {
            var dogs = await _mapperService.MapEntitiesAsync(new List<DogDTO>());

            Assert.NotNull(dogs);
            Assert.Empty(dogs);
        }
        #endregion
    }
}
