using Microsoft.AspNetCore.Mvc;
using PetConnect.Application.Services;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly IAdoptionQueryService _queryService;
        private readonly IAdoptionService _adoptionService;
        private readonly IShelterQueryService _shelterQueryService;
        private readonly IAnimalQueryService _animalQueryService; 
        private readonly IAdopterQueryService _adopterQueryService;
        public AdoptionController(IAdoptionQueryService queryService, IAdoptionService adoptionService, IShelterQueryService shelterQueryService, IAnimalQueryService animalQueryService, IAdopterQueryService adopterQueryService)
        {
            _queryService = queryService;
            _adoptionService = adoptionService;
            _shelterQueryService = shelterQueryService;
            _animalQueryService = animalQueryService;
            _adopterQueryService = adopterQueryService;
        }
        public async Task<IActionResult> Index()
        {
            var adoptionList = await _queryService.GetAdoptionListAsync();
            return View(adoptionList);
        }

        //GET Details Adoption
        public async Task<IActionResult> Details(int id)
        {
            var adoption = await _queryService.GetAdoptionDetailsAsync(id);
            return View(adoption);
        }

        //GET Create Adoption
        public async Task<IActionResult> Create()
        {
            var viewModel = new AdoptionViewModel
            {
                Shelters = await _shelterQueryService.GetSelectListItemsAsync(),
                Adopters = await _adopterQueryService.GetSelectListItemsAsync(),
                Animals = await _animalQueryService.GetSelectListItemsAsync(),
               
            };

            return View(viewModel);
        }

        //POST Create Adoption
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionViewModel viewModel)
        {
          if(!ModelState.IsValid)
            {
                viewModel.Shelters = await _shelterQueryService.GetSelectListItemsAsync();
                viewModel.Adopters = await _adopterQueryService.GetSelectListItemsAsync();
                viewModel.Animals = await _animalQueryService.GetSelectListItemsAsync();
                
                return View(viewModel);
            }

            var success = await _adoptionService.CreateAsync(viewModel);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));

        }

        //GET Update Adoption
        public async Task<IActionResult> Update(int id)
        {
            var viewModel = await _queryService.GetAdoptionForUpdateAsync(id);
            if (viewModel == null) return NotFound();

            viewModel.Shelters = await _shelterQueryService.GetSelectListItemsAsync();
            viewModel.Adopters = await _adopterQueryService.GetSelectListItemsAsync();
            viewModel.Animals = await _animalQueryService.GetSelectListItemsAsync();

            return View(viewModel);
        }

        //POST Update Adoption
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdoptionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                 var success = await _adoptionService.UpdateAsync(viewModel);
                 if(!success) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        //POST Deactivate Adoption
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var success = await _adoptionService.DeactivateAsync(id);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
