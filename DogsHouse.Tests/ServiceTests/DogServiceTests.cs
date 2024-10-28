using DogsHouse.Services;
using DogsHouse.Services.DataPresentation;
using DogsHouse.Services.Model;
using DogsHouse.Services.Utility.ApiResult;
using DogsHouse.Tests.Utility;

namespace DogsHouse.Tests.ServiceTests
{
    public class DogServiceTests
    {
        private readonly IDogService _dogService;
        public DogServiceTests()
        {
            _dogService = MockUtility.MockDogService();
        }

        #region AddDog
        [Fact]
        public void AddDog_ShouldCreateAndAddDog()
        {
            var dogDto = TestData.GetTestDogDTO(0, "test0");
            var result = _dogService.AddDog(dogDto);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiOkResult), result.GetType());
        }

        [Fact]
        public async Task AddDogAsync_ShouldCreateAndAddDog()
        {
            var dogDto = TestData.GetTestDogDTO(0, "test0");
            var result = await _dogService.AddDogAsync(dogDto);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiOkResult), result.GetType());
        }

        [Fact]
        public void AddDog_ShouldFailToValidate()
        {
            var dogDto = TestData.GetTestDogDTO(0, "test0", "");
            var result = _dogService.AddDog(dogDto);

            Assert.NotNull(result);
            Assert.Equal(ApiResultStatus.ValidationFailed, result.ApiResultStatus);
        }

        [Fact]
        public async Task AddDogAsync_ShouldFailToValidate()
        {
            var dogDto = TestData.GetTestDogDTO(0, "test0", "");
            var result = await _dogService.AddDogAsync(dogDto);

            Assert.NotNull(result);
            Assert.Equal(ApiResultStatus.ValidationFailed, result.ApiResultStatus);
        }

        [Fact]
        public void AddDog_ShouldBeConflict()
        {
            var dogDto = TestData.GetTestDogDTO(0, "Jessy");
            var result = _dogService.AddDog(dogDto);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiErrorResult), result.GetType());
            Assert.Equal(ApiResultStatus.Conflict, result.ApiResultStatus);
        }

        [Fact]
        public async Task AddDogAsync_ShouldBeConflict()
        {
            var dogDto = TestData.GetTestDogDTO(0, "Jessy");
            var result = await _dogService.AddDogAsync(dogDto);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiErrorResult), result.GetType());
            Assert.Equal(ApiResultStatus.Conflict, result.ApiResultStatus);
        }

        [Fact]
        public void AddDog_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _dogService.AddDog(null));
        }

        [Fact]
        public void AddDogAsync_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _dogService.AddDogAsync(null));
        }
        #endregion

        #region DeleteDog
        [Fact]
        public void DeleteDog_ShouldRemoveDog()
        {
            var result = _dogService.DeleteDog(1);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiOkResult), result.GetType());
        }

        [Fact]
        public async Task DeleteDogASync_ShouldRemoveDog()
        {
            var result = await _dogService.DeleteDogAsync(1);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiOkResult), result.GetType());
        }

        [Fact]
        public void DeleteDog_DogNotFound()
        {
            var result = _dogService.DeleteDog(5);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiErrorResult), result.GetType());
            Assert.Equal(ApiResultStatus.NotFound, result.ApiResultStatus);
        }

        [Fact]
        public async Task DeleteDogASync_DogNotFound()
        {
            var result = await _dogService.DeleteDogAsync(5);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiErrorResult), result.GetType());
            Assert.Equal(ApiResultStatus.NotFound, result.ApiResultStatus);
        }
        #endregion

        #region FilterDogs
        [Fact]
        public void FilterDogs_ShouldFilterDogs()
        {
            var filter = new DogsSortingFilter { Attribute = "name", Order = "desc" };

            var result = _dogService.FilterDogs(filter);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiOkResult), result.GetType());
            Assert.NotNull(((ApiOkResult)result).Data);
            Assert.NotEmpty(((ApiOkResult)result).Data as IEnumerable<DogDTO>);
        }

        [Fact]
        public async Task FilterDogsAsync_ShouldFilterDogs()
        {
            var filter = new DogsSortingFilter { Attribute = "name", Order = "desc" };

            var result = await _dogService.FilterDogsAsync(filter);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiOkResult), result.GetType());
            Assert.NotNull(((ApiOkResult)result).Data);
            Assert.NotEmpty(((ApiOkResult)result).Data as IEnumerable<DogDTO>);
        }

        [Fact]
        public void FilterDogs_FilterValidationFailed()
        {
            var filter = new DogsSortingFilter { Attribute = "123", Order = "up" };

            var result = _dogService.FilterDogs(filter);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiErrorResult), result.GetType());
            Assert.Equal(ApiResultStatus.ValidationFailed, result.ApiResultStatus);
        }

        [Fact]
        public async Task FilterDogsASync_FilterValidationFailed()
        {
            var filter = new DogsSortingFilter { Attribute = "123", Order = "up" };

            var result = await _dogService.FilterDogsAsync(filter);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiErrorResult), result.GetType());
            Assert.Equal(ApiResultStatus.ValidationFailed, result.ApiResultStatus);
        }
        #endregion
    }
}
