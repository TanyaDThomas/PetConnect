using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Identity;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;
using System.Net.NetworkInformation;
using static Azure.Core.HttpHeader;

namespace PetConnect.Application.Services
{
    public class AdoptionQueryService : IAdoptionQueryService
    {
        private readonly PetConnectDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AdoptionQueryService(PetConnectDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

   
        public async Task<IEnumerable<Adoption>> GetAllAsync()
        {
            return await _context.Adoptions
                .AsNoTracking()
                .Where(a => a.IsActive)
                .ToListAsync();
        }

    
        public async Task<Adoption?> GetByIdAsync(int id)
        {
            return await _context.Adoptions.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<AdoptionListViewModel>> GetAdoptionListAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");



            var query = _context.Adoptions
                .AsNoTracking()
                .AsSplitQuery()
                .Include(a => a.Shelter)
                .Include(a => a.Adopter)
                .Include(a => a.Animal)
                    .ThenInclude(animal => animal.AnimalType)
                .Where(a => a.IsActive);

          

            if (!isAdmin)
            {
                var userShelterIds = await _context.UserShelters
                    .Where(us => us.UserId == userId && us.IsActive)
                    .Select(us => us.ShelterId)
                    .ToListAsync();

                query = query.Where(a => userShelterIds.Contains(a.ShelterId));
            }

            var adoptions = await query.ToListAsync();

            return adoptions.Select(a => new AdoptionListViewModel
            {
                Id = a.Id,
                ShelterName = a.Shelter?.Name ?? "",
                AdopterName = a.Adopter?.FullName ?? "",
                AnimalType = a.Animal?.AnimalType?.Name ?? "",
                AnimalName = a.Animal?.Name ?? "",
                AdoptionDate = a.AdoptionDate,
                Status = a.Status
            }).ToList();

        }

        public async Task<AdoptionViewModel?> GetAdoptionForUpdateAsync(int id)
        {
            var adoption = await _context.Adoptions.FirstOrDefaultAsync(a => a.Id == id);
            if (adoption == null) return null;

            return new AdoptionViewModel
            {
                Id = adoption.Id,
                ShelterId = adoption.ShelterId,
                AdopterId = adoption.AdopterId,
                AnimalId = adoption.AnimalId,
                Status = adoption.Status,
                AdoptionFee = adoption.AdoptionFee
            };
        }

        public async Task<AdoptionDetailsViewModel?> GetAdoptionDetailsAsync(int id)
        {
            var adoption = await _context.Adoptions
                .AsNoTracking()
                .AsSplitQuery()
                .Include(a => a.Shelter)
                .Include(a => a.Adopter)
                .Include(a => a.Animal)
                    .ThenInclude(animal => animal.AnimalType)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (adoption == null) return null;

            var notes = await _context.Notes
                .AsNoTracking()
                .Where(n => n.EntityType == NoteEntityType.Adoption
                         && n.EntityId == id)
                .OrderByDescending(n => n.CreatedOn)
                .Take(5)
                .ToListAsync();

            return new AdoptionDetailsViewModel
            {
                Id = adoption.Id,
                ShelterName = adoption.Shelter?.Name ?? "",
                AnimalType = adoption.Animal?.AnimalType?.Name ?? "",
                AnimalName = adoption.Animal?.Name ?? "",
                AdoptionFee = adoption.AdoptionFee,
                AdoptionDate = adoption.AdoptionDate,
                Status = adoption.Status,
                RecentNotes = notes
            };
 
        }

       
    }
}
