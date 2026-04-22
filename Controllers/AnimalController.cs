using Microsoft.AspNetCore.Mvc;
using PetConnect.Application.Services;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IAnimalQueryService _queryService;
        private readonly IAnimalService _animalService;
        private readonly IShelterQueryService _shelterQueryService;
        private readonly IAnimalTypeQueryService _animalTypeQueryService;
        public AnimalController(IAnimalQueryService queryService, IAnimalService animalService, IShelterQueryService shelterQueryService, IAnimalTypeQueryService animalTypeQueryService)
        {
            _queryService = queryService;
            _animalService = animalService;
            _shelterQueryService = shelterQueryService;
            _animalTypeQueryService = animalTypeQueryService;
        }
     
        public async Task<IActionResult> Index()
        {
            var viewModel = await _queryService.GetAnimalListAsync();
            return View(viewModel);
        }

        //GET Details Animal
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _queryService.GetAnimalDetailsAsync(id);


            return View(viewModel);
        }

        //GET Create Animal 
        public async Task<IActionResult> Create()
        {
            var viewModel = new AnimalViewModel
            {
                Shelters = await _shelterQueryService.GetSelectListItemsAsync(),
                AnimalTypes = await _animalTypeQueryService.GetSelectListItemsAsync()
            };

            return View(viewModel);
        }

        //POST Create Animal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Shelters = await _shelterQueryService.GetSelectListItemsAsync();

                viewModel.AnimalTypes = await _animalTypeQueryService.GetSelectListItemsAsync();

                return View(viewModel);
            }
            var success = await _animalService.CreateAsync(viewModel);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        //GET Update Animal
        public async Task<IActionResult> Update(int id)
        {
            var viewModel = await _queryService.GetAnimalUpdateAsync(id);
           
            return View(viewModel);
        }

        //POST Update Animal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AnimalViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var success = await _animalService.UpdateAsync(viewModel);
                if(!success) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        //POST Deactivate Animal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var success = await _animalService.DeactivateAsync(id);
            if(!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }

    }
}
