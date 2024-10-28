using DogsHouse.Extensions;
using DogsHouse.Services;
using DogsHouse.Services.DataPresentation;
using DogsHouse.Services.Model;
using DogsHouse.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DogsHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogService dogsService;
        private readonly ILogger<DogsController> logger;

        public DogsController(IDogService dogsService, ILogger<DogsController> logger)
        {
            this.dogsService = dogsService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var apiResult = await dogsService.GetDogsAsync();

            return this.ActionResultByApiResult(apiResult, logger);
        }

        [HttpGet("ping")]
        public IActionResult Ping([FromServices] IOptions<ApplicationOptions> application)
        {
            var appInfo = $"{application.Value.Name}. Version {application.Value.Version}";

            return Ok(appInfo);
        }

        [HttpGet("filter-dogs")]
        public async Task<IActionResult> FilterDogs([FromQuery] DogsSortingFilter filter)
        {
            var apiResult = await dogsService.FilterDogsAsync(filter);

            return this.ActionResultByApiResult(apiResult, logger);
        }

        [HttpGet("get-paged-dogs")]
        public async Task<IActionResult> GetPagedDogs([FromQuery] DogsPagination pagination)
        {
            var apiResult = await dogsService.GetPagedAsync(pagination);

            return this.ActionResultByApiResult(apiResult, logger);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DogDTO dog)
        {
            var apiResult = await dogsService.AddDogAsync(dog);

            return this.ActionResultByApiResult(apiResult, logger);
        }

        [HttpPut]
        public async Task<IActionResult> Put(DogDTO newDog)
        {
            var apiResult = await dogsService.UpdateDogAsync(newDog);

            return this.ActionResultByApiResult(apiResult, logger);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var apiResult = await dogsService.DeleteDogAsync(id);

            return this.ActionResultByApiResult(apiResult, logger);
        }
    }
}
