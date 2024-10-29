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
            var dogDto = TestDataUtility.GetTestDogDTO(0, "test0");
            var result = _dogService.AddDog(dogDto);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiOkResult), result.GetType());
        }

        [Fact]
        public async Task AddDogAsync_ShouldCreateAndAddDog()
        {
            var dogDto = TestDataUtility.GetTestDogDTO(0, "test0");
            var result = await _dogService.AddDogAsync(dogDto);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiOkResult), result.GetType());
        }

        [Fact]
        public void AddDog_ShouldFailToValidate()
        {
            var dogDto = TestDataUtility.GetTestDogDTO(0, "test0", "");
            var result = _dogService.AddDog(dogDto);

            Assert.NotNull(result);
            Assert.Equal(ApiResultStatus.ValidationFailed, result.ApiResultStatus);
        }

        [Fact]
        public async Task AddDogAsync_ShouldFailToValidate()
        {
            var dogDto = TestDataUtility.GetTestDogDTO(0, "test0", "");
            var result = await _dogService.AddDogAsync(dogDto);

            Assert.NotNull(result);
            Assert.Equal(ApiResultStatus.ValidationFailed, result.ApiResultStatus);
        }

        [Fact]
        public void AddDog_ShouldBeConflict()
        {
            var dogDto = TestDataUtility.GetTestDogDTO(0, "Jessy");
            var result = _dogService.AddDog(dogDto);

            Assert.NotNull(result);
            Assert.Equal(typeof(ApiErrorResult), result.GetType());
            Assert.Equal(ApiResultStatus.Conflict, result.ApiResultStatus);
        }

        [Fact]
        public async Task AddDogAsync_ShouldBeConflict()
        {
            var dogDto = TestDataUtility.GetTestDogDTO(0, "Jessy");
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

        [Fact]
        public void DeleteDog_ThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => _dogService.DeleteDog(null));
        }

        [Fact]
        public void DeleteDogAsync_ThrowsNullReferenceException()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () => await _dogService.DeleteDogAsync(null));
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

        [Fact]
        public void FilterDogs_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _dogService.FilterDogs(null));
        }

        [Fact]
        public void FilterDogsAsync_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _dogService.FilterDogsAsync(null));
        }
        #endregion

        #region GetDogs
        [Fact]
        public void GetDogs_ShouldListDogs()
        {
            var result = _dogService.GetDogs();

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.Ok, result.ApiResultStatus);
            Assert.NotEmpty(((IApiOkResult)result).Data as IEnumerable<DogDTO>);
        }

        [Fact]
        public async Task GetDogsAsync_ShouldListDogs()
        {
            var result = await _dogService.GetDogsAsync();

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.Ok, result.ApiResultStatus);
            Assert.NotEmpty(((IApiOkResult)result).Data as IEnumerable<DogDTO>);
        }

        [Fact]
        public void GetDogs_ShouldReturnNoContent()
        {
            // Simulate an empty db
            var localDogService = MockUtility.MockDogServiceEmptyDb();

            var result = localDogService.GetDogs();

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.NoContent, result.ApiResultStatus);
        }

        [Fact]
        public async Task GetDogsAsync_ShouldReturnNoContent()
        {
            // Simulate an empty db
            var localDogService = MockUtility.MockDogServiceEmptyDb();

            var result = await localDogService.GetDogsAsync();

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.NoContent, result.ApiResultStatus);
        }
        #endregion

        #region GetPaged
        [Fact]
        public void GetPaged_ShouldReturnPagedDogs()
        {
            var pagination = new DogsPagination { Page = 1, PageSize = 3 };

            var result = _dogService.GetPaged(pagination);

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.Ok, result.ApiResultStatus);
            Assert.NotEmpty(((IApiOkResult)result).Data as IEnumerable<DogDTO>);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedDogs()
        {
            var pagination = new DogsPagination { Page = 1, PageSize = 3 };

            var result = await _dogService.GetPagedAsync(pagination);

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.Ok, result.ApiResultStatus);
            Assert.NotEmpty(((IApiOkResult)result).Data as IEnumerable<DogDTO>);
        }

        [Fact]
        public void GetPaged_ShouldReturnNoContentIfDbIsEmpty()
        {
            // Simulate an empty db
            var localDogService = MockUtility.MockDogServiceEmptyDb();
            var pagination = new DogsPagination { Page = 1, PageSize = 3 };

            var result = localDogService.GetPaged(pagination);

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.NoContent, result.ApiResultStatus);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnNoContentIfDbIsEmpty()
        {
            // Simulate an empty db
            var localDogService = MockUtility.MockDogServiceEmptyDb();
            var pagination = new DogsPagination { Page = 1, PageSize = 3 };

            var result = await localDogService.GetPagedAsync(pagination);

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.NoContent, result.ApiResultStatus);
        }

        [Fact]
        public void GetPaged_ShouldReturnNoContentByGivenPagination()
        {
            var pagination = new DogsPagination { Page = 3, PageSize = 10 };

            var result = _dogService.GetPaged(pagination);

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.NoContent, result.ApiResultStatus);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnNoContentByGivenPagination()
        {
            var pagination = new DogsPagination { Page = 3, PageSize = 10 };

            var result = await _dogService.GetPagedAsync(pagination);

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.NoContent, result.ApiResultStatus);
        }

        [Fact]
        public void GetPaged_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _dogService.GetPaged(null));
        }

        [Fact]
        public void GetPagedAsync_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _dogService.GetPagedAsync(null));
        }
        #endregion

        #region UpdateDog
        [Fact]
        public void UpdateDog_ShouldUpdateDog()
        {
            var newDogDto = TestDataUtility.GetTestDogDTO(1, "Jessy", "black&white", weight: 20);

            var result = _dogService.UpdateDog(newDogDto);

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.Ok, result.ApiResultStatus);
            Assert.Equal(newDogDto.Weight, (((IApiOkResult)result).Data as DogDTO).Weight);
        }

        [Fact]
        public async Task UpdateDogAsync_ShouldUpdateDog()
        {
            var newDogDto = TestDataUtility.GetTestDogDTO(1, "Jessy", "black&white", weight: 20);

            var result = await _dogService.UpdateDogAsync(newDogDto);

            Assert.NotNull(result);
            Assert.IsType<ApiOkResult>(result);
            Assert.Equal(ApiResultStatus.Ok, result.ApiResultStatus);
            Assert.Equal(newDogDto.Weight, (((IApiOkResult)result).Data as DogDTO).Weight);
        }

        [Fact]
        public void UpdateDog_ShouldReturnNotFound()
        {
            var newDogDto = TestDataUtility.GetTestDogDTO(4, "Jessy", "black&white", weight: 20);

            var result = _dogService.UpdateDog(newDogDto);

            Assert.NotNull(result);
            Assert.IsType<ApiErrorResult>(result);
            Assert.Equal(ApiResultStatus.NotFound, result.ApiResultStatus);
        }

        [Fact]
        public async Task UpdateDogAsync_ShouldReturnNotFound()
        {
            var newDogDto = TestDataUtility.GetTestDogDTO(4, "Jessy", "black&white", weight: 20);

            var result = await _dogService.UpdateDogAsync(newDogDto);

            Assert.NotNull(result);
            Assert.IsType<ApiErrorResult>(result);
            Assert.Equal(ApiResultStatus.NotFound, result.ApiResultStatus);
        }

        [Fact]
        public void UpdateDog_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _dogService.UpdateDog(null));
        }

        [Fact]
        public void UpdateDogAsync_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _dogService.UpdateDogAsync(null));
        }
        #endregion
    }
}
