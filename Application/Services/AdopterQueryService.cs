using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Identity;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PetConnect.Application.Services
{
    public class AdopterQueryService : IAdopterQueryService
    {
        private readonly PetConnectDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AdopterQueryService(PetConnectDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //GET LIST FOR INDEX
        public async Task<List<AdopterListViewModel>> GetAdopterListAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var query = _context.Adopters
                .AsNoTracking()
                .Include(a => a.Shelter)
                .Where(a => a.IsActive);

            if (!isAdmin)
            {
                var userShelterIds = await _context.UserShelters
                    .Where(us => us.UserId == userId && us.IsActive)
                    .Select(us => us.ShelterId)
                    .ToListAsync();

                query = query.Where(a => userShelterIds.Contains(a.ShelterId));
            }

            var adopters = await query.ToListAsync();

            return adopters.Select(a => new AdopterListViewModel
            {
                Id = a.Id,
                FullName = $"{a.FirstName} {a.LastName}",
                PhoneNumber = a.PhoneNumber,
                Email = a.Email,
                ShelterName = a.Shelter?.Name ?? ""
            }).ToList();

            //var userShelterIds = await _context.UserShelters
            //    .Where(us => us.UserId == userId && us.IsActive)
            //    .Select(us => us.ShelterId)
            //    .ToListAsync();


            //var adopters = await _context.Adopters
            //    .AsNoTracking()
            //    .Include(a => a.Shelter)
            //    .Where(a => a.IsActive)
            //    .Where(a => userShelterIds.Contains(a.ShelterId))
            //    .ToListAsync();

            //return adopters.Select(a => new AdopterListViewModel
            //{
            //    Id = a.Id,
            //    FullName = $"{a.FirstName} {a.LastName}",
            //    PhoneNumber = a.PhoneNumber,
            //    Email = a.Email,
            //    ShelterName = a.Shelter?.Name ?? ""

            //}).ToList();
        }

        //GET LIST FOR DETAILS
        public async Task<AdopterViewModel?> GetAdopterDetailsAsync(int id)
        {
            var adopter = await _context.Adopters
                .AsNoTracking()
                .Include(a => a.Shelter)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (adopter == null) return null;

            var notes = await _context.Notes
                .AsNoTracking()
                .Where(n => n.EntityType == NoteEntityType.Adopter
                         && n.EntityId == id)
                .OrderByDescending(n => n.CreatedOn)
                .Take(5)
                .ToListAsync();

            return new AdopterViewModel
            {
                Id = adopter.Id,
                FirstName = adopter.FirstName,
                LastName = adopter.LastName,
                Address = adopter.Address,
                City = adopter.City,
                State = adopter.State,
                PostalCode = adopter.PostalCode,
                PhoneNumber = adopter.PhoneNumber,
                Email = adopter.Email,
                HasChildren = adopter.HasChildren,
                HasOtherPets = adopter.HasOtherPets,
                HasYard = adopter.HasYard,
                ShelterId = adopter.ShelterId,
                RecentNotes = notes,



                ShelterName = adopter.Shelter?.Name ?? ""
            };
        }

        //GET UPDATE SERVICE
        public async Task<AdopterViewModel?> GetAdopterForUpdateAsync(int id)
        {
            var adopter = await _context.Adopters.FirstOrDefaultAsync(a => a.Id == id);
            if (adopter == null) return null;

            return new AdopterViewModel
            {
                Id = adopter.Id,
                FirstName = adopter.FirstName,
                LastName = adopter.LastName,
                Address = adopter.Address,
                City = adopter.City,
                State = adopter.State,
                PostalCode = adopter.PostalCode,
                PhoneNumber = adopter.PhoneNumber,
                Email = adopter.Email,
                HasOtherPets = adopter.HasOtherPets,
                HasChildren = adopter.HasChildren,
                HasYard = adopter.HasYard,
                ShelterId = adopter.ShelterId,

            };

        }



        public async Task<IEnumerable<Adopter>> GetAllAsync()
        {    
            return await _context.Adopters
               .AsNoTracking()
               .Include(a => a.Shelter)
               .Where(a => a.IsActive)
               .ToListAsync();
        }

        public async Task<Adopter?> GetByIdAsync(int id)
        {
            return await _context.Adopters.FirstOrDefaultAsync(a => a.Id == id);
            
        }

        public async Task<List<SelectListItem>> GetSelectListItemsAsync()
        {
            var adopters = await GetAllAsync();

            return adopters.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FullName
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetSelectListBySheltersAsync(List<int> shelterIds)
        {
            return await _context.Adopters
                .AsNoTracking()
                .Where(a => a.IsActive && shelterIds.Contains(a.ShelterId))
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.FirstName} {a.LastName}"
                })
                .ToListAsync();
        }
    }
}
