using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Contracts;

namespace PetConnect.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsApiController : ControllerBase
    {
        private readonly IAnimalQueryService _animalQueryService;

        public AnimalsApiController(IAnimalQueryService animalQueryService)
        {
            _animalQueryService = animalQueryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
