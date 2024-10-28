using DogsHouse.Database.Model;
using DogsHouse.Services.Abstraction;
using DogsHouse.Services.DataPresentation;
using DogsHouse.Services.Model;
using DogsHouse.Services.Utility;
using DogsHouse.Services.Utility.ApiResult;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DogsHouse.Services
{
    public class DogService : IDogService
    {
        private readonly IExtendedEntityService<Dog> dogEntityService;
        private readonly IMapperService<Dog, DogDTO> mapperService;
        private readonly IValidator<DogDTO> dogValidator;
        private readonly IValidator<DogsSortingFilter> dogsFilterValidator;
        private readonly IValidator<DogsPagination> dogsPaginationValidator;
        private readonly ILogger<IDogService> logger;

        public DogService(
            IExtendedEntityService<Dog> entityService,
            IMapperService<Dog,
            DogDTO> mapperService,
            IValidator<DogDTO> dogValidator,
            IValidator<DogsSortingFilter> dogsFilterValidator,
            IValidator<DogsPagination> dogsPaginationValidator,
            ILogger<IDogService> logger)
        {
            this.dogEntityService = entityService;
            this.mapperService = mapperService;
            this.dogValidator = dogValidator;
            this.dogsFilterValidator = dogsFilterValidator;
            this.dogsPaginationValidator = dogsPaginationValidator;
            this.logger = logger;
        }
        public IApiResult AddDog(DogDTO dog)
        {
            var apiResult = default(IApiResult);
            var errorMessage = "Could not add a new dog";
            var validationResult = ValidateDog(dog);

            if (!validationResult.IsValid)
            {
                logger.LogError($"{errorMessage}. Failed to validate dog DTO");
                var validationErrors = validationResult.Errors.Select(err => err.ErrorMessage);
                apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: validationErrors);
            }
            else
            {
                var existingDog = dogEntityService.ReadByCondition(d => d.Name == dog.Name);

                if (existingDog != null)
                {
                    errorMessage = $"{errorMessage}. Dog already exists in DB. (Name: {existingDog.Name})";
                    logger.LogError(errorMessage);
                    apiResult = new ApiErrorResult(ApiResultStatus.Conflict, loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: [errorMessage]);
                }
                else
                {
                    var dogToAdd = mapperService.MapEntity(dog);

                    if (dogToAdd == null)
                    {
                        logger.LogError($"{errorMessage}. Failed to map entity from DTO");
                        apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: [errorMessage]);
                    }
                    else
                    {
                        var addResult = dogEntityService.Create(dogToAdd);

                        if (!addResult)
                        {
                            logger.LogError($"{errorMessage}. Failed to add to DB");
                            apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: [errorMessage]);
                        }
                        else
                        {
                            var message = "Successfully added a new dog";
                            logger.LogInformation($"{message}. (Dog name - {dogToAdd.Name})");
                            apiResult = new ApiOkResult(ApiResultStatus.NoContent, message: message);
                        }
                    }
                }
            }

            return apiResult;
        }

        public async Task<IApiResult> AddDogAsync(DogDTO dog)
        {
            return await Task.FromResult(AddDog(dog));
        }

        public IApiResult DeleteDog(object id)
        {
            var apiResult = default(IApiResult);
            var errorMessage = "Could not delete a dog";

            var dogToDelete = dogEntityService.ReadById(id);

            if (dogToDelete == null)
            {
                logger.LogError($"{errorMessage}. Failed to find a dog (Id = {id})");
                apiResult = new ApiErrorResult(ApiResultStatus.NotFound, loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: [errorMessage]);
            }
            else
            {
                var deleteResult = dogEntityService.Delete(dogToDelete);

                if (!deleteResult)
                {
                    logger.LogError($"{errorMessage}. Failed to delete a dog (Id = {id})");
                    apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: [errorMessage]);
                }
                else
                {
                    var message = "Successfully deleted a dog";
                    logger.LogInformation($"{message}. (Dog name - {dogToDelete.Name})");
                    apiResult = new ApiOkResult(ApiResultStatus.NoContent, message: message);
                }
            }

            return apiResult;
        }

        public async Task<IApiResult> DeleteDogAsync(object id)
        {
            return await Task.FromResult(DeleteDog(id));
        }

        public IApiResult FilterDogs(DogsSortingFilter filter)
        {
            var apiResult = default(IApiResult);
            var errorMessage = "Could not retreive dogs by filter";
            var filterMessage = $"(Attribute: {filter.Attribute}). (Order: {filter.Order})";

            var dogsFilterValidationResult = dogsFilterValidator.Validate(filter);

            if (!dogsFilterValidationResult.IsValid)
            {
                logger.LogError($"{errorMessage}. Failed to validate dog filter. {filterMessage}");
                var validationErrors = dogsFilterValidationResult.Errors.Select(err => err.ErrorMessage);
                apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: validationErrors);
            }
            else
            {
                var sortFunc = SortingFuncFromFilter.GetFunc(filter);
                var dogs = default(IEnumerable<Dog>);

                dogs = filter.Order.ToLower() switch
                {
                    "asc" => [.. dogEntityService.Read().OrderBy(sortFunc)],
                    _ => [.. dogEntityService.Read().OrderByDescending(sortFunc)]
                };

                if (dogs == null || !dogs.Any())
                {
                    logger.LogError($"{errorMessage}. Failed to sort dogs by a given filter. {filterMessage}");
                    var validationErrors = dogsFilterValidationResult.Errors.Select(err => err.ErrorMessage);
                    apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: validationErrors);
                }
                else
                {
                    var message = "Successfully sorted dogs";
                    logger.LogInformation($"{message}. {filterMessage}");
                    var dogDtos = mapperService.MapDtos(dogs);
                    apiResult = new ApiOkResult(ApiResultStatus.Ok, message: message, data: dogDtos);
                }
            }

            return apiResult;
        }

        public async Task<IApiResult> FilterDogsAsync(DogsSortingFilter filter)
        {
            return await Task.FromResult(FilterDogs(filter));
        }

        public IApiResult GetDogs()
        {
            var apiResult = default(IApiResult);
            var loggerErrorMessage = "Could not retreive dogs";

            var dogs = dogEntityService.Read().ToArray();

            if (dogs.Length == 0)
            {
                logger.LogWarning($"{loggerErrorMessage}. No dogs in DB");
                apiResult = new ApiOkResult(ApiResultStatus.NoContent, "No dogs to show");
            }
            else
            {
                var message = "Successfully retreived dogs from DB";
                logger.LogInformation(message);
                var dogDtos = mapperService.MapDtos(dogs);
                apiResult = new ApiOkResult(message: message, data: dogDtos);
            }

            return apiResult;
        }

        public async Task<IApiResult> GetDogsAsync()
        {
            return await Task.FromResult(GetDogs());
        }

        public IApiResult GetPaged(DogsPagination pagination)
        {
            var apiResult = default(IApiResult);
            var errorMessage = "Could not retreive a portion of dogs";

            var validationResult = dogsPaginationValidator.Validate(pagination);

            if (!validationResult.IsValid)
            {
                logger.LogError($"{errorMessage}. Failed to validate dog pagination");
                var validationErrors = validationResult.Errors.Select(err => err.ErrorMessage);
                apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: validationErrors);
            }
            else
            {
                var count = dogEntityService.Read().Count();

                if (count == 0)
                {
                    logger.LogWarning($"{errorMessage}. No dogs in DB");
                    apiResult = new ApiOkResult(ApiResultStatus.NoContent, "No dogs to show");
                }
                else
                {
                    var skip = pagination.GetSkip();
                    var take = pagination.GetTake();

                    var pagedDogs = dogEntityService.ReadPortion(skip, take).ToArray();

                    if (pagedDogs.Length == 0)
                    {
                        logger.LogWarning($"{errorMessage}. (Page: {pagination.Page}). (Page size: {pagination.PageSize})");
                        apiResult = new ApiOkResult(ApiResultStatus.NoContent, "No dogs to show");
                    }
                    else
                    {
                        var message = "Successfully retreived a portion of dogs from DB";
                        logger.LogInformation($"{message}. Count = ({pagedDogs.Length})");
                        var pagedDogsDtos = mapperService.MapDtos(pagedDogs);
                        apiResult = new ApiOkResult(message: message, data: pagedDogsDtos);
                    }
                }
            }

            return apiResult;
        }

        public async Task<IApiResult> GetPagedAsync(DogsPagination pagination)
        {
            return await Task.FromResult(GetPaged(pagination));
        }

        public IApiResult UpdateDog(DogDTO dog)
        {
            var apiResult = default(IApiResult);
            var errorMessage = "Could not update a dog";
            var dogIdMessage = $"(Id - {dog.Id})";

            var validationResult = ValidateDog(dog);

            if (!validationResult.IsValid)
            {
                logger.LogError($"{errorMessage}. Failed to validate dog DTO");
                var validationErrors = validationResult.Errors.Select(err => err.ErrorMessage);
                apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: validationErrors);
            }
            else
            {

                var dogFromDto = mapperService.MapEntity(dog);

                if (dogFromDto == null)
                {
                    logger.LogError($"{errorMessage}. Failed to map entity from DTO");
                    apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: [errorMessage]);
                }
                else
                {
                    var dogToUpdate = dogEntityService.ReadById(dog.Id);

                    if (dogToUpdate == null)
                    {
                        logger.LogError($"{errorMessage}. Failed to find a dog. {dogIdMessage}");
                        apiResult = new ApiErrorResult(ApiResultStatus.NotFound, loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: [errorMessage]);
                    }
                    else
                    {
                        var updatedDog = dogEntityService.Update(dogToUpdate, dogFromDto);

                        if (updatedDog == null)
                        {
                            logger.LogError($"{errorMessage}. {dogIdMessage}");
                            apiResult = new ApiErrorResult(loggerErrorMessage: errorMessage, errorMessage: errorMessage, errors: [errorMessage]);
                        }
                        else
                        {
                            var message = "Successfully updated a dog";
                            logger.LogInformation($"{message}. Dog: {dogIdMessage}, (Name - {updatedDog.Name})");
                            var updatedDogDto = mapperService.MapDto(updatedDog);
                            apiResult = new ApiOkResult(ApiResultStatus.Ok, message: message, data: updatedDogDto);
                        }
                    }
                }
            }

            return apiResult;
        }

        public async Task<IApiResult> UpdateDogAsync(DogDTO dog)
        {
            return await Task.FromResult(UpdateDog(dog));
        }

        private FluentValidation.Results.ValidationResult ValidateDog(DogDTO dog)
        {
            return dogValidator.Validate(dog);
        }
    }
}
