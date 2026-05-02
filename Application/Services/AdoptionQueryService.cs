using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;
using static Azure.Core.HttpHeader;

namespace PetConnect.Application.Services
{
    public class AdoptionQueryService : IAdoptionQueryService
    {
        private readonly PetConnectDbContext _context;
        public AdoptionQueryService(PetConnectDbContext context)
        {
            _context = context;
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

        public async Task<List<AdoptionListViewModel>> GetAdoptionListAsync()
        {
            var adoptions = await _context.Adoptions
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Include(a => a.Shelter)
                    .Include(a => a.Adopter)
                    .Include(a => a.Animal)
                        .ThenInclude(animal => animal.AnimalType)
                    .Where(a => a.IsActive)
                    .ToListAsync();

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
