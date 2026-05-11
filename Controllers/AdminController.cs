using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Identity;
using PetConnect.ViewModels;



namespace PetConnect.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserShelterQueryService _userShelterQueryService;
        private readonly IUserShelterService _userShelterService;
        private readonly IShelterQueryService _shelterQueryService;

        public AdminController(UserManager<AppUser> userManager, IUserShelterQueryService userShelterQueryService, IUserShelterService userShelterService, IShelterQueryService shelterQueryService)
        {
            _userManager = userManager;
            _userShelterQueryService = userShelterQueryService;
            _userShelterService = userShelterService;
            _shelterQueryService = shelterQueryService;
        }
        public async Task<IActionResult> Index()
        {
            
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            

            List<UserShelter> assignments;

                if (User.IsInRole("Admin"))
                {
                    assignments = await _userShelterQueryService.GetUserShelterListAsync();
                }
                else
                {
                    
                    assignments = await _userShelterQueryService.GetAssignmentsForUserAsync(userId);
                }

                return View(assignments);

        }



        // GET Assign User - Form

        public async Task<IActionResult> Create()
        {
            var shelters = await _shelterQueryService.GetShelterListAsync();

            var viewModel = new AssignUserShelterViewModel
            {
           
                Users = _userManager.Users.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.Email
                }).ToList(),

                Shelters = shelters.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} - {s.City}, {s.State}"
                }).ToList(),

                Roles = new List<SelectListItem>
                {
                    new SelectListItem { Value = ShelterRoles.Manager, Text = "Manager" },
                    new SelectListItem { Value = ShelterRoles.Staff, Text = "Staff"}
                }

            };

            return View(viewModel);

        }



       
        //POST Create Assingment - submit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignUserShelterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var success = await _userShelterService.CreateAsync(
                viewModel.UserId,
                viewModel.ShelterId,
                viewModel.RoleInShelter);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }


    
        //GET Update assignment
        public async Task<IActionResult> Update(string userId, int shelterId)
        {
            var existing = await _userShelterQueryService
                .GetByUserAndShelterAsync(userId, shelterId);

            if (existing == null) return NotFound();

            var shelters = await _shelterQueryService.GetShelterListAsync();

            var viewModel = new AssignUserShelterViewModel
            {
                UserId = existing.UserId,
                ShelterId = existing.ShelterId,
                RoleInShelter = existing.RoleInShelter,

                Users = _userManager.Users.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.Email
                }),

                Shelters = shelters.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} - {s.City}, {s.State}"
                })
            };

            return View(viewModel);
        }

       

        //POST Update Assignment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AssignUserShelterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var success = await _userShelterService.UpdateAsync(
                viewModel.UserId,
                viewModel.ShelterId,
                viewModel.RoleInShelter);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }


        //POST Deactivate UserShelter

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(string userId, int shelterId)
        {
            var success = await _userShelterService.DeactivateAsync(userId, shelterId);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}
