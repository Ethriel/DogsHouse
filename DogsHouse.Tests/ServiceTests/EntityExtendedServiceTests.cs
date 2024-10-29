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

        #region ReadByCondition
        [Fact]
        public void ReadByCondition_ShouldReturnDog()
        {
            var name = "Jessy";
            var dog = _entityExtendedService.ReadByCondition(dog => dog.Name == name);

            Assert.NotNull(dog);
            Assert.Equal(name, dog.Name);
        }

        [Fact]
        public async Task ReadByConditionAsync_ShouldReturnDog()
        {
            var name = "Jessy";
            var dog = await _entityExtendedService.ReadByConditionAsync(dog => dog.Name == name);

            Assert.NotNull(dog);
            Assert.Equal(name, dog.Name);
        }

        [Fact]
        public void ReadByCondition_ShouldReturnNull()
        {
            var name = "Gerbert";
            var dog = _entityExtendedService.ReadByCondition(dog => dog.Name == name);

            Assert.Null(dog);
        }

        [Fact]
        public async Task ReadByConditionAsync_ShouldReturnNull()
        {
            var name = "Gerbert";
            var dog = await _entityExtendedService.ReadByConditionAsync(dog => dog.Name == name);

            Assert.Null(dog);
        }

        [Fact]
        public void ReadByCondition_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _entityExtendedService.ReadByCondition(null));
        }

        [Fact]
        public void ReadByConditionAsync_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _entityExtendedService.ReadByConditionAsync(null));
        }
        #endregion

        #region ReadById
        
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
        public void ReadById_ShouldReturnNull()
        {
            var dog = _entityExtendedService.ReadById(4);

            Assert.Null(dog);
        }

        [Fact]
        public async Task ReadByIdAsync_ShouldReturnNull()
        {
            var dog = await _entityExtendedService.ReadByIdAsync(4);

            Assert.Null(dog);
        }

        [Fact]
        public void ReadById_ShouldThrowNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => _entityExtendedService.ReadById(null));
        }

        [Fact]
        public void ReadByIdAsync_ShouldThrowNullReferenceException()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () => await _entityExtendedService.ReadByIdAsync(null));
        }
        #endregion

        #region ReadEntitiesByCondition
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
        public void ReadEntitiesByCondition_ShouldBeEmptyResult()
        {
            var name = "Gerbert";
            var dogs = _entityExtendedService.ReadEntitiesByCondition(dog => dog.Name == name);

            Assert.Empty(dogs);
        }

        [Fact]
        public async Task ReadEntitiesByConditionAsync_ShouldBeEmptyResult()
        {
            var name = "Gerbert";
            var dogs = await _entityExtendedService.ReadEntitiesByConditionAsync(dog => dog.Name == name);

            Assert.Empty(dogs);
        }

        [Fact]
        public void ReadEntitiesByCondition_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _entityExtendedService.ReadEntitiesByCondition(null));
        }

        [Fact]
        public void ReadEntitiesByConditionAsync_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _entityExtendedService.ReadEntitiesByConditionAsync(null));
        }
        #endregion

        #region ReadPortion
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

        [Fact]
        public void ReadPortion_ShouldReturnEmptyResult()
        {
            var portionOfDogs = _entityExtendedService.ReadPortion(10, 10);

            Assert.NotNull(portionOfDogs);
            Assert.Empty(portionOfDogs);
        }

        [Fact]
        public async Task ReadPortionAsync_ShouldReturnEmptyResult()
        {
            var portionOfDogs = await _entityExtendedService.ReadPortionAsync(10, 10);

            Assert.NotNull(portionOfDogs);
            Assert.Empty(portionOfDogs);
        }
        #endregion
    }
}
