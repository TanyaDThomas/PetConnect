using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Application.Services;
using PetConnect.Domain.Contracts;
using PetConnect.Infrastructure.Identity;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    [Authorize]
    public class AdoptionController : Controller
    {
        private readonly IAdoptionQueryService _queryService;
        private readonly IAdoptionService _adoptionService;
        private readonly IShelterQueryService _shelterQueryService;
        private readonly IAnimalQueryService _animalQueryService; 
        private readonly IAdopterQueryService _adopterQueryService;
        private readonly UserManager<AppUser> _userManager;
        public AdoptionController(IAdoptionQueryService queryService, IAdoptionService adoptionService, IShelterQueryService shelterQueryService, IAnimalQueryService animalQueryService, IAdopterQueryService adopterQueryService, UserManager<AppUser> userManager)
        {
            _queryService = queryService;
            _adoptionService = adoptionService;
            _shelterQueryService = shelterQueryService;
            _animalQueryService = animalQueryService;
            _adopterQueryService = adopterQueryService;
            _userManager = userManager;
           
        }

        public async Task<IActionResult> Index(AdoptionSearchFilter filter, string? searchTerm)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var adoptionList = await _queryService.GetAdoptionListAsync(userId);

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                adoptionList = adoptionList
                    .Where(x =>
                        x.AdopterName.Contains(filter.SearchTerm) ||
                        x.AnimalName.Contains(filter.SearchTerm))
                    .ToList();
            }

            var query = adoptionList.AsQueryable();

            
           
            if (filter.AdoptionDateFrom.HasValue)
            {
                query = query.Where(x =>
                    x.AdoptionDate >= filter.AdoptionDateFrom.Value);
            }

            
            if (filter.AdoptionDateTo.HasValue)
            {
                query = query.Where(x =>
                    x.AdoptionDate <= filter.AdoptionDateTo.Value);
            }

            var result = query.ToList();

            var viewModel = new AdoptionIndexViewModel
            {
                Adoptions = result,
                Filter = filter,
                TotalCount = adoptionList.Count,
                FilteredCount = result.Count
            };

            return View(viewModel);
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
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            List<SelectListItem> shelters;
            List<int> shelterIds;

            if (User.IsInRole("Admin"))
            {
                shelters = await _shelterQueryService.GetSelectListItemsAsync();

                // get all shelter ids via service (NOT DbContext)
                var allShelters = await _shelterQueryService.GetShelterListAsync();
                shelterIds = allShelters.Select(s => s.Id).ToList();
            }
            else
            {
                var allowedShelters = await _shelterQueryService.GetSheltersForManagerAsync(userId);

                shelters = allowedShelters.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} - {s.City}, {s.State}"
                }).ToList();

                shelterIds = allowedShelters.Select(s => s.Id).ToList();
            }

            var viewModel = new AdoptionViewModel
            {
                Shelters = shelters,
                Adopters = await _adopterQueryService.GetSelectListBySheltersAsync(shelterIds),
                Animals = await _animalQueryService.GetSelectListBySheltersAsync(shelterIds)
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

            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var success = await _adoptionService.CreateAsync(viewModel, userId);
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
                var userId = _userManager.GetUserId(User);
                if(userId == null) return Unauthorized();

                 var success = await _adoptionService.UpdateAsync(viewModel, userId);
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
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var success = await _adoptionService.DeactivateAsync(id, userId);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
