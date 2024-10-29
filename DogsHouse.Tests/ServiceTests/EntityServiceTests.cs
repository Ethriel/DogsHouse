using DogsHouse.Database.Model;
using DogsHouse.Services;
using DogsHouse.Tests.Utility;
using System.Reflection;

namespace DogsHouse.Tests.ServiceTests
{
    public class EntityServiceTests
    {
        private readonly IEntityService<Dog> _entityService;

        public EntityServiceTests()
        {
            _entityService = MockUtility.MockDogEntityService();
        }

        #region Read
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
        public void Read_ShouldReturnAnEmptyResult()
        {
            // Simulate an empty db
            var localEntityService = MockUtility.MockDogEntityServiceEmptyDb();

            var dogs = localEntityService.Read();

            Assert.NotNull(dogs);
            Assert.Empty(dogs);
        }

        [Fact]
        public async Task ReadAsync_ShouldReturnAnEmptyResult()
        {
            // Simulate an empty db
            var localEntityService = MockUtility.MockDogEntityServiceEmptyDb();

            var dogs = await localEntityService.ReadAsync();

            Assert.NotNull(dogs);
            Assert.Empty(dogs);
        }
        #endregion

        #region Create
        [Fact]
        public void Create_ShoudAddDog()
        {
            var newDog = TestDataUtility.GetTestDog();

            var createResult = _entityService.Create(newDog);
            var dogs = _entityService.Read();

            Assert.True(createResult);
            Assert.Equal(4, dogs.Count());
        }

        [Fact]
        public async Task CreateAsync_ShoudAddDog()
        {
            var newDog = TestDataUtility.GetTestDog();
            var createResult = await _entityService.CreateAsync(newDog);

            var dogs = await _entityService.ReadAsync();

            Assert.True(createResult);
            Assert.Equal(4, dogs.Count());
        }

        [Fact]
        public void Create_ShouldFailToAddNull()
        {
            var res = _entityService.Create(null);
            Assert.False(res);
        }

        [Fact]
        public async Task CreateAsync_ShouldFailToAddNull()
        {
            var res = await _entityService.CreateAsync(null);
            Assert.False(res);
        }
        #endregion

        #region Delete
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
        #endregion

        #region Update
        [Fact]
        public void Update_ShouldUpdateDog()
        {
            var oldDog = _entityService.Read().FirstOrDefault(dog => dog.Name == "Jessy");
            var newDog = TestDataUtility.GetTestDog(1, "Jessy", "black&white", 10, 18);

            var updatedDog = _entityService.Update(oldDog, newDog);

            Assert.NotNull(updatedDog);
            Assert.Equal(updatedDog.Weight, newDog.Weight);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateDog()
        {
            var dogs = await _entityService.ReadAsync();
            var oldDog = dogs.FirstOrDefault(dog => dog.Name == "Jessy");
            var newDog = TestDataUtility.GetTestDog(1, "Jessy", "black&white", 10, 18);

            var updatedDog = await _entityService.UpdateAsync(oldDog, newDog);

            Assert.NotNull(updatedDog);
            Assert.Equal(updatedDog.Weight, newDog.Weight);
        }

        [Fact]
        public void Update_ThrowsTargetExceptionIfOldDogIsNull()
        {
            var newDog = TestDataUtility.GetTestDog(1, "Jessy", "black&white", 10, 18);

            Assert.Throws<TargetException>(() => _entityService.Update(null, newDog));
        }

        [Fact]
        public void UpdateAsync_ThrowsTargetExceptionIfOldDogIsNull()
        {
            var newDog = TestDataUtility.GetTestDog(1, "Jessy", "black&white", 10, 18);

            Assert.ThrowsAsync<TargetException>(async () => await _entityService.UpdateAsync(null, newDog));
        }

        [Fact]
        public void Update_ThrowsNullReferenceExceptionIfNewDogIsNull()
        {
            var oldDog = _entityService.Read().FirstOrDefault(dog => dog.Name == "Jessy");

            Assert.Throws<NullReferenceException>(() => _entityService.Update(oldDog, null));
        }

        [Fact]
        public void UpdateAsync_ThrowsNullReferenceExceptionIfNewDogIsNull()
        {
            var oldDog = _entityService.Read().FirstOrDefault(dog => dog.Name == "Jessy");

            Assert.ThrowsAsync<NullReferenceException>(async () => await _entityService.UpdateAsync(oldDog, null));
        }

        [Fact]
        public void Update_ThrowsNullReferenceExceptionIfBothAreNull()
        {
            Assert.Throws<NullReferenceException>(() => _entityService.Update(null, null));
        }

        [Fact]
        public void UpdateAsync_ThrowsNullReferenceExceptionIfBothAreNull()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () => await _entityService.UpdateAsync(null, null));
        }
        #endregion
    }
}