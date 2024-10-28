using DogsHouse.Services.DataPresentation;
using DogsHouse.Services.Model;
using DogsHouse.Services.Utility.ApiResult;

namespace DogsHouse.Services
{
    public interface IDogService
    {
        IApiResult GetDogs();
        Task<IApiResult> GetDogsAsync();
        IApiResult AddDog(DogDTO dog);
        Task<IApiResult> AddDogAsync(DogDTO dog);
        IApiResult DeleteDog(object id);
        Task<IApiResult> DeleteDogAsync(object id);
        IApiResult UpdateDog(DogDTO dog);
        Task<IApiResult> UpdateDogAsync(DogDTO dog);
        IApiResult GetPaged(DogsPagination pagination);
        Task<IApiResult> GetPagedAsync(DogsPagination pagination);
        IApiResult FilterDogs(DogsSortingFilter filter);
        Task<IApiResult> FilterDogsAsync(DogsSortingFilter filter);
    }
}
