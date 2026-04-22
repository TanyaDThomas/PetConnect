using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;

namespace PetConnect.Controllers
{
    public class AnimalTypeController : Controller
    {
        private readonly IAnimalTypeQueryService _queryService;
        private readonly IAnimalTypeService _typeService;
        public AnimalTypeController(IAnimalTypeQueryService queryService, IAnimalTypeService typeService)
        {
            _queryService = queryService;
            _typeService = typeService;
        }
        public async Task<IActionResult> Index()
        {
            var animalTypeList = await _queryService.GetAllAsync();
            return View(animalTypeList);
        }

        //GET Create AnimalType
        public IActionResult Create()
        {
            return View();
        }

        //POST Create AnimalType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalType animalType)
        {
            if(ModelState.IsValid)
            {
                var success = await _typeService.CreateAsync(animalType);
                if (!success) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(animalType);
        }

        //POST Delete AnimalType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _typeService.DeleteAsync(id);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
