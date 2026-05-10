using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Identity;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
   [Authorize]
    public class ShelterController : Controller
    {
        private readonly IShelterQueryService _queryService;
        private readonly IShelterService _shelterService;
        private readonly UserManager<AppUser> _userManager;
        public ShelterController(IShelterQueryService queryService, IShelterService shelterService, UserManager<AppUser> userManager)
        {
            _queryService = queryService;
            _shelterService = shelterService;
            _userManager = userManager;
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
                var userId = _userManager.GetUserId(User);
                if (userId == null) return Unauthorized();

                await _shelterService.CreateAsync(viewModel, userId);
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
                var userId = _userManager.GetUserId(User);
                if(userId == null) return Unauthorized();

                await _shelterService.UpdateAsync(viewModel, userId);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        //POST Deactivate Shelter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var userId = _userManager.GetUserId(User);
            if(userId ==null) return Unauthorized();

            var success = await _shelterService.DeactivateAsync(id, userId);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
