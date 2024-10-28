using DogsHouse.Database.Model;
using DogsHouse.Services.Abstraction;
using DogsHouse.Services.Model;
using DogsHouse.Tests.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouse.Tests.ServiceTests
{
    public class MapperServiceTests
    {
        private readonly IMapperService<Dog, DogDTO> _mapperService;

        public MapperServiceTests()
        {
            _mapperService = MockUtility.MockMapperService();
        }

        [Fact]
        public void MapDto_MappsEntityToDTO()
        {
            var dog = new Dog
            {
                Id = 1,
                Color = "black&white",
                Name = "Jessy",
                TailLength = 10,
                Weight = 15
            };

            var dogDto = _mapperService.MapDto(dog);

            Assert.NotNull(dogDto);
            Assert.True(dog.Id == dogDto.Id && dog.Name == dogDto.Name);
        }

        [Fact]
        public async Task MapDtoAsync_MappsEntityToDTO()
        {
            var dog = new Dog
            {
                Id = 1,
                Color = "black&white",
                Name = "Jessy",
                TailLength = 10,
                Weight = 15
            };

            var dogDto = await _mapperService.MapDtoAsync(dog);

            Assert.NotNull(dogDto);
            Assert.True(dog.Id == dogDto.Id && dog.Name == dogDto.Name);
        }

        [Fact]
        public void MapDtos_MappsEntitiesToDTOs()
        {
            var dogs = TestData.GetTestDogs();
            var dogDtos = _mapperService.MapDtos(dogs);

            Assert.NotNull(dogDtos);
            Assert.NotEmpty(dogDtos);
            Assert.True(dogs.FirstOrDefault()?.Id == dogDtos.FirstOrDefault()?.Id && dogs.First().Name == dogDtos.FirstOrDefault()?.Name);
        }

        [Fact]
        public async Task MapDtosAsync_MappsEntitiesToDTOs()
        {
            var dogs = TestData.GetTestDogs();
            var dogDtos = await _mapperService.MapDtosAsync(dogs);

            Assert.NotNull(dogDtos);
            Assert.NotEmpty(dogDtos);
            Assert.True(dogs.FirstOrDefault()?.Id == dogDtos.FirstOrDefault()?.Id && dogs.First().Name == dogDtos.FirstOrDefault()?.Name);
        }

        [Fact]
        public void MapEntity_MappsEntityFromDTO()
        {
            var dogDto = new DogDTO
            {
                Id = 1,
                Color = "black&white",
                Name = "Jessy",
                TailLength = 10,
                Weight = 15
            };

            var dog = _mapperService.MapEntity(dogDto);

            Assert.NotNull(dog);
            Assert.True(dog.Id == dogDto.Id && dog.Name == dogDto.Name);
        }

        [Fact]
        public async Task MapEntityAsync_MappsEntityFromDTO()
        {
            var dogDto = new DogDTO
            {
                Id = 1,
                Color = "black&white",
                Name = "Jessy",
                TailLength = 10,
                Weight = 15
            };

            var dog = await _mapperService.MapEntityAsync(dogDto);

            Assert.NotNull(dog);
            Assert.True(dog.Id == dogDto.Id && dog.Name == dogDto.Name);
        }

        [Fact]
        public void MapEntities_MappsEntitiesFromDTOs()
        {
            var dogDtos = TestData.GetTestDogDTOs();
            var dogs = _mapperService.MapEntities(dogDtos);

            Assert.NotNull(dogDtos);
            Assert.NotEmpty(dogDtos);
            Assert.True(dogs.FirstOrDefault()?.Id == dogDtos.FirstOrDefault()?.Id && dogs.First().Name == dogDtos.FirstOrDefault()?.Name);
        }

        [Fact]
        public async Task MapEntitiesAsync_MappsEntitiesFromDTOs()
        {
            var dogDtos = TestData.GetTestDogDTOs();
            var dogs = await _mapperService.MapEntitiesAsync(dogDtos);

            Assert.NotNull(dogDtos);
            Assert.NotEmpty(dogDtos);
            Assert.True(dogs.FirstOrDefault()?.Id == dogDtos.FirstOrDefault()?.Id && dogs.First().Name == dogDtos.FirstOrDefault()?.Name);
        }
    }
}
