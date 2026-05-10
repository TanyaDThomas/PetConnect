using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetConnect.Application.Services;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Identity;
using PetConnect.ViewModels;
using SQLitePCL;
using System.Runtime.InteropServices;

namespace PetConnect.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly IAnimalQueryService _queryService;
        private readonly IAnimalService _animalService;
        private readonly IShelterQueryService _shelterQueryService;
        private readonly IAnimalTypeQueryService _animalTypeQueryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IShelterAuthorizationService _auth;

        public AnimalController(IAnimalQueryService queryService, IAnimalService animalService, IShelterQueryService shelterQueryService, IAnimalTypeQueryService animalTypeQueryService, UserManager<AppUser> userManager, IShelterAuthorizationService auth)
        {
            _queryService = queryService;
            _animalService = animalService;
            _shelterQueryService = shelterQueryService;
            _animalTypeQueryService = animalTypeQueryService;
            _userManager = userManager;
            _auth = auth;
        }

    
        public async Task<IActionResult> Index(AnimalSearchFilter filter)
        {
            var viewModel = await _queryService.GetAnimalListAsync(filter);
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

            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            if (!await _auth.CanManageShelterAsync(userId, viewModel.ShelterId))
                return Forbid();


            var success = await _animalService.CreateAsync(viewModel, userId);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        //GET Update Animal
        public async Task<IActionResult> Update(int id)
        {
            var viewModel = await _queryService.GetAnimalUpdateAsync(id);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        //POST Update Animal
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AnimalViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Shelters = await _shelterQueryService.GetSelectListItemsAsync();
                viewModel.AnimalTypes = await _animalTypeQueryService.GetSelectListItemsAsync();

                return View(viewModel);
            }

            var userId = _userManager.GetUserId(User);
            if(userId == null) return Unauthorized();

            var existingAnimal = await _queryService.GetByIdAsync(viewModel.Id);
            if (existingAnimal == null) return NotFound();

            if (!await _auth.CanManageShelterAsync(userId, viewModel.ShelterId))
                return Forbid();

            var success = await _animalService.UpdateAsync(viewModel, userId);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        //POST Deactivate Animal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var userId = _userManager.GetUserId(User); 
            if (userId == null) return NotFound();

            var animal = await _queryService.GetByIdAsync(id);
            if(animal == null) return NotFound();

            if (!await _auth.CanManageShelterAsync(userId, animal.ShelterId))
                return Forbid();

            var success = await _animalService.DeactivateAsync(id, userId);
            if(!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }

    }
}
