using DogsHouse.Database.Model;
using DogsHouse.Services;
using DogsHouse.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DogsHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IEntityService<Dog> dogsService;

        public DogsController(IEntityService<Dog> dogsService)
        {
            this.dogsService = dogsService;
        }
        // GET: api/<DogsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dogs = await dogsService.ReadAsync();

            return Ok(dogs);
        }

        // GET api/<DogsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dogs = await dogsService.ReadAsync();
            var dog = dogs.FirstOrDefault(x => x.Id == id);

            return Ok(dog);
        }

        [HttpGet("ping")]
        public async Task<IActionResult> Ping([FromServices] IOptions<Application> application)
        {
            var appInfo = $"{application.Value.Name}. Version {application.Value.Version}";

            return Ok(appInfo);
        }

        // POST api/<DogsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dog dog)
        {
            var result = await dogsService.CreateAsync(dog);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Dog newDog)
        {
            var dogs = await dogsService.ReadAsync();
            var oldDog = dogs.FirstOrDefault(x => x.Name == newDog.Name);
            newDog = await dogsService.UpdateAsync(oldDog, newDog);

            return Ok(newDog);
        }

        // DELETE api/<DogsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await dogsService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
