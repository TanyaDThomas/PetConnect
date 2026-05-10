using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Identity;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    [Authorize]
    public class AdopterController : Controller
    {
        private readonly IAdopterQueryService _queryService;
        private readonly IAdopterService _adopterService;
        private readonly IShelterQueryService _shelterQueryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IShelterAuthorizationService _auth;

        public AdopterController(IAdopterQueryService queryService, IAdopterService adopterService, IShelterQueryService shelterQueryService, UserManager<AppUser> userManager, IShelterAuthorizationService auth)
        {
            _queryService = queryService;
            _adopterService = adopterService;
            _shelterQueryService = shelterQueryService;
            _userManager = userManager;
            _auth = auth;
        }
        public async Task<IActionResult> Index()
        {
            var adopterList = await _queryService.GetAdopterListAsync();

            return View(adopterList);
        }

        //GET Details Adopter
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _queryService.GetAdopterDetailsAsync(id);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        //GET Create Adopter
        public async Task<IActionResult> Create()
        {
            var viewModel = new AdopterViewModel
            {
                Shelters = await _shelterQueryService.GetSelectListItemsAsync()
            };

            return View(viewModel);   
        }

        //POST Create Adopter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdopterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Shelters = await _shelterQueryService.GetSelectListItemsAsync();

                return View(viewModel);
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null) return Unauthorized();
            

            var success = await _adopterService.CreateAsync(viewModel, userId);
            if(!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        //GET Update Adopter
        public async Task<IActionResult> Update(int id)
        {
            var viewModel = await _queryService.GetAdopterForUpdateAsync(id);
            if (viewModel == null) return NotFound();

            viewModel.Shelters = await _shelterQueryService.GetSelectListItemsAsync();

            return View(viewModel);
        }

        //POST Update Adopter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdopterViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Shelters = await _shelterQueryService.GetSelectListItemsAsync();
                return View(viewModel);
            }

            var userId = _userManager.GetUserId(User);
            if(userId == null) return Unauthorized();

            var success = await _adopterService.UpdateAsync(viewModel, userId);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        //POST Deactivate Adopter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var success = await _adopterService.DeactivateAsync(id, userId);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
