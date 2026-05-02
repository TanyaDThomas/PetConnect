using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Application.Services
{
    public class AdopterQueryService : IAdopterQueryService
    {
        private readonly PetConnectDbContext _context;
        public AdopterQueryService(PetConnectDbContext context)
        {
            _context = context;
        }

        //GET LIST FOR INDEX
        public async Task<List<AdopterListViewModel>> GetAdopterListAsync()
        {
            var adopters = await _context.Adopters
                .AsNoTracking()
                .Include(a => a.Shelter)
                .Where(a => a.IsActive)
                .ToListAsync();

            return adopters.Select(a => new AdopterListViewModel
            {
                Id = a.Id,
                FullName = $"{a.FirstName} {a.LastName}",
                PhoneNumber = a.PhoneNumber,
                Email = a.Email,
                ShelterName = a.Shelter?.Name ?? ""

            }).ToList();
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
    }
}
