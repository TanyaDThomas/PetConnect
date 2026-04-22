using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    public class ShelterController : Controller
    {
        private readonly IShelterQueryService _queryService;
        private readonly IShelterService _shelterService;
        public ShelterController(IShelterQueryService queryService, IShelterService shelterService)
        {
            _queryService = queryService;
            _shelterService = shelterService;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = await _queryService.GetShelterListAsync();
            return View(viewModel);
        }

        //GET Details Shelter
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _queryService.GetShelterDetailsAsync(id);
            if (viewModel == null) return NotFound();
            return View(viewModel);
        }

        //GET Create Shelter
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Shelter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShelterViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                await _shelterService.CreateAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        //GET Update Shelter
        public async Task<IActionResult> Update(int id)
        {
            var viewModel = await _queryService.GetShelterUpdateAsync(id);
            return View(viewModel);
           
        }

        //POST Update Shelter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ShelterViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                await _shelterService.UpdateAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        //POST Deactivate Shelter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var success = await _shelterService.DeactivateAsync(id);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
