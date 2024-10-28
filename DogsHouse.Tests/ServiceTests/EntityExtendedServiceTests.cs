using DogsHouse.Database.Model;
using DogsHouse.Services;
using DogsHouse.Tests.Utility;

namespace DogsHouse.Tests.ServiceTests
{
    public class EntityExtendedServiceTests
    {
        private readonly IEntityExtendedService<Dog> _entityExtendedService;
        public EntityExtendedServiceTests()
        {
            _entityExtendedService = MockUtility.MockDogEntityExtendedService();
        }

        [Fact]
        public void ReadByCondition_ShouldReturnDog()
        {
            var name = "Jessy";
            var dog = _entityExtendedService.ReadByCondition(x => x.Name == "Jessy");

            Assert.NotNull(dog);
            Assert.Equal(name, dog.Name);
        }

        [Fact]
        public async Task ReadByConditionAsync_ShouldReturnDog()
        {
            var name = "Jessy";
            var dog = await _entityExtendedService.ReadByConditionAsync(dog => dog.Name == "Jessy");

            Assert.NotNull(dog);
            Assert.Equal(name, dog.Name);
        }

        [Fact]
        public void ReadById_ShouldReturnDog()
        {
            var id = 1;
            var dog = _entityExtendedService.ReadById(id);

            Assert.NotNull(dog);
            Assert.Equal(id, dog.Id);
        }

        [Fact]
        public async Task ReadByIdAsync_ShouldReturnDog()
        {
            var id = 1;
            var dog = await _entityExtendedService.ReadByIdAsync(id);

            Assert.NotNull(dog);
            Assert.Equal(id, dog.Id);
        }

        [Fact]
        public void ReadEntitiesByCondition_ShouldReturnDogs()
        {
            var dogs = _entityExtendedService.ReadEntitiesByCondition(dog => dog.TailLength >= 10);

            Assert.NotNull(dogs);
            Assert.NotEmpty(dogs);
        }

        [Fact]
        public async Task ReadEntitiesByConditionAsync_ShouldReturnDogs()
        {
            var dogs = await _entityExtendedService.ReadEntitiesByConditionAsync(dog => dog.TailLength >= 10);

            Assert.NotNull(dogs);
            Assert.NotEmpty(dogs);
        }

        [Fact]
        public void ReadPortion_ShouldReturnAPortonOfDogs()
        {
            var allDogs = _entityExtendedService.Read();

            var portionOfDogs = _entityExtendedService.ReadPortion(1, 2);

            Assert.NotNull(portionOfDogs);
            Assert.NotEmpty(portionOfDogs);
            Assert.NotEqual(allDogs.Count(), portionOfDogs.Count());
        }

        [Fact]
        public async Task ReadPortionAsync_ShouldReturnAPortonOfDogs()
        {
            var allDogs = await _entityExtendedService.ReadAsync();

            var portionOfDogs = await _entityExtendedService.ReadPortionAsync(1, 2);

            Assert.NotNull(portionOfDogs);
            Assert.NotEmpty(portionOfDogs);
            Assert.NotEqual(allDogs.Count(), portionOfDogs.Count());
        }
    }
}
