using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Application.Services
{
    public class ShelterQueryService : IShelterQueryService
    {
        private readonly PetConnectDbContext _context;
        public ShelterQueryService(PetConnectDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Shelter>> GetAllAsync()
        {
            return await _context.Shelters
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<Shelter?> GetByIdAsync(int id)
        {
           return await _context.Shelters.FirstOrDefaultAsync(x => x.Id == id);

        }

        // SELECTLIST
        public async Task<List<SelectListItem>> GetSelectListItemsAsync()
        {
            var shelters = await GetAllAsync();

            return shelters.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
        }


        //GET INDEX SHELTER
        public async Task<IEnumerable<ShelterListViewModel>> GetShelterListAsync()
        {
            var shelters = await _context.Shelters
                    .AsNoTracking()
                    .Where(s => s.IsActive)
                    .ToListAsync();

            return shelters.Select(s => new ShelterListViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                City = s.City,
                State = s.State,
                PhoneNumber = s.PhoneNumber
             }).ToList();
        }



        //GET DETAILS SHELTER
        public async Task<ShelterViewModel?> GetShelterDetailsAsync(int id)
        {
            var shelter = await _context.Shelters.FirstOrDefaultAsync(s => s.Id == id);
            if (shelter == null) return null;

            var notes = await _context.Notes
                .AsNoTracking()
                .Where(n => n.EntityType == NoteEntityType.Shelter
                         && n.EntityId == id)
                .OrderByDescending(n => n.CreatedOn)
                .Take(5)
                .ToListAsync();

            return new ShelterViewModel
            {
                Id = shelter.Id,
                Name = shelter.Name,
                Address = shelter.Address,
                City = shelter.City,
                State = shelter.State,
                PostalCode = shelter.PostalCode,
                PhoneNumber= shelter.PhoneNumber,
                Email = shelter.Email,
                IsActive = shelter.IsActive,
                RecentNotes = notes
            };
               
        }

   

        //GET UPDATE SHELTER
        public async Task<ShelterViewModel?> GetShelterUpdateAsync(int id)
        {
            var shelter = _context.Shelters.FirstOrDefault(s => s.Id == id);
            if(shelter == null) return null;

            return new ShelterViewModel
            {
                Id = shelter.Id,
                Name = shelter.Name,
                Address = shelter.Address,
                City = shelter.City,
                State = shelter.State,
                PostalCode = shelter.PostalCode,
                PhoneNumber = shelter.PhoneNumber,
                Email = shelter.Email
            };
        }


    }
}
