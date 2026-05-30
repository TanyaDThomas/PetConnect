using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PetConnect.Application.Services;
using PetConnect.Application.Services.DTOs;
using PetConnect.Domain.Contracts;
using static System.Net.WebRequestMethods;

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

        //GET ANIMALS API
        [HttpGet("Search")]
        public async Task<IActionResult> AnimalSearchAsync([FromQuery] AnimalApiSearchFilter filter)
        {
            var result = await _animalQueryService.ApiSearchAsync(filter);
            return Ok(result);
   
        }

     
    }
    
}
