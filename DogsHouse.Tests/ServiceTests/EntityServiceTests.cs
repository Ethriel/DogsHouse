using DogsHouse.Database.Model;
using DogsHouse.Services;
using DogsHouse.Tests.Utility;

namespace DogsHouse.Tests.ServiceTests
{
    public class EntityServiceTests
    {
        private readonly IEntityService<Dog> _entityService;

        public EntityServiceTests()
        {
            _entityService = MockUtility.MockDogEntityService();
        }
        [Fact]
        public void Read_ShouldGetDogsFromDb()
        {
            var dogs = _entityService.Read();

            Assert.NotNull(dogs);
            Assert.NotEmpty(dogs);
        }
        [Fact]
        public async Task ReadAsync_ShouldGetDogsFromDb()
        {
            var dogs = await _entityService.ReadAsync();

            Assert.NotNull(dogs);
            Assert.NotEmpty(dogs);
        }
        [Fact]
        public void Create_ShoudAddDog()
        {
            var newDog = TestData.GetTestDog();

            var createResult = _entityService.Create(newDog);
            var dogs = _entityService.Read();

            Assert.True(createResult);
            Assert.Equal(4, dogs.Count());
        }

        [Fact]
        public async Task CreateAsync_ShoudAddDog()
        {
            var newDog = TestData.GetTestDog();
            var createResult = await _entityService.CreateAsync(newDog);

            var dogs = await _entityService.ReadAsync();

            Assert.True(createResult);
            Assert.Equal(4, dogs.Count());
        }

        [Fact]
        public void Delete_ShouldRemoveDog()
        {
            var removeResult = _entityService.Delete(1);
            var dogs = _entityService.Read();

            Assert.True(removeResult);
            Assert.Equal(2, dogs.Count());
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveDog()
        {
            var removeResult = await _entityService.DeleteAsync(1);
            var dogs = await _entityService.ReadAsync();

            Assert.True(removeResult);
            Assert.Equal(2, dogs.Count());
        }

        [Fact]
        public void Delete_ShouldReturnFalseIfDogNotFound()
        {
            var removeResult = _entityService.Delete(4);
            var dogs = _entityService.Read();

            Assert.False(removeResult);
            Assert.Equal(3, dogs.Count());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalseIfDogNotFound()
        {
            var removeResult = await _entityService.DeleteAsync(4);
            var dogs = await _entityService.ReadAsync();

            Assert.False(removeResult);
            Assert.Equal(3, dogs.Count());
        }

        [Fact]
        public void Update_ShouldUpdateDog()
        {
            var oldDog = _entityService.Read().FirstOrDefault(dog => dog.Name == "Jessy");

            var newDog = new Dog
            {
                Id = 1,
                Color = "black&white",
                Name = "Jessy",
                TailLength = 10,
                Weight = 18
            };

            var updatedDog = _entityService.Update(oldDog, newDog);

            Assert.NotNull(updatedDog);
            Assert.Equal(updatedDog.Weight, newDog.Weight);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateDog()
        {
            var dogs = await _entityService.ReadAsync();
            var oldDog = dogs.FirstOrDefault(dog => dog.Name == "Jessy");

            var newDog = new Dog
            {
                Id = 1,
                Color = "black&white",
                Name = "Jessy",
                TailLength = 10,
                Weight = 18
            };

            var updatedDog = await _entityService.UpdateAsync(oldDog, newDog);

            Assert.NotNull(updatedDog);
            Assert.Equal(updatedDog.Weight, newDog.Weight);
        }
    }
}