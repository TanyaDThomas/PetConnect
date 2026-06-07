using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Identity;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly PetConnectDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public DashboardController(PetConnectDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public async Task<IActionResult> Index()
        {
           
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

   
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            UserShelter? userShelter = null;

            
            if (!isAdmin)
            {
                userShelter = _context.UserShelters
                    .FirstOrDefault(us => us.UserId == user.Id && us.IsActive);

                if (userShelter == null)
                    return Forbid();
            }

          
            IQueryable<Animal> animals = _context.Animals;
            IQueryable<Adoption> adoptions = _context.Adoptions;
            IQueryable<Payment> payments = _context.Payments;

         
            if (!isAdmin)
            {
                animals = animals.Where(a => a.ShelterId == userShelter!.ShelterId);

                adoptions = adoptions.Where(a => a.ShelterId == userShelter!.ShelterId);

                payments = payments.Where(p => p.Adoption.ShelterId == userShelter!.ShelterId);
            }

           
            var viewModel = new DashboardViewModel
            {
                TotalAnimals = animals.Count(a => a.IsActive),
                Adoptions = adoptions.Count(a => a.IsActive),
                PendingAdoptions = adoptions.Count(a =>a.Status == AdoptionStatus.Pending && a.IsActive),

                TotalRevenue = payments
                    .Sum(p => (decimal?)p.Amount) ?? 0,

            };

            return View(viewModel);
        }


    }
}
