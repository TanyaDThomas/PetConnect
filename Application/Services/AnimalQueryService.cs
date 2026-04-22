using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Application.Services
{
    public class AnimalQueryService : IAnimalQueryService
    {
        private readonly PetConnectDbContext _context;
        
        public AnimalQueryService(PetConnectDbContext context)
        {
            _context = context;
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - dob.Year;

            if (dob.Date > today.AddYears(-age))
                age--;

            return age;
        }


        public async Task<decimal?> GetAnimalFeeAsync(int animalId)
        {
            return await _context.Animals
                .Where(a => a.Id == animalId)
                .Select(a => a.AdoptionFee)
                .FirstOrDefaultAsync();
        }


        // GET LIST FOR INDEX

        public async Task<IEnumerable<AnimalListViewModel>> GetAnimalListAsync()
        {
            var animals = await _context.Animals
                .AsNoTracking()
                .Include(a => a.Shelter)
                .Include(a => a.AnimalType)
                .Where(a => a.IsActive)
                .ToListAsync();

            return animals.Select(a => new AnimalListViewModel
            {
                Id = a.Id,
                Name = a.Name,
                AnimalTypeName = a.AnimalType?.Name ?? "",
                Breed = a.Breed,
                IsAdopted = a.IsAdopted,
                ShelterName = a.Shelter?.Name ?? ""

            }).ToList();
        }

        //GET DETAILS
        public async Task<AnimalViewModel?> GetAnimalDetailsAsync(int id)
        {
            var animal = await _context.Animals
                    .AsNoTracking()
                    .Include(a => a.Shelter)
                    .Where(a => a.IsActive)
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null) return null;

            return new AnimalViewModel
            {
                Id = animal.Id,

                ShelterId = animal.ShelterId,
                AnimalTypeId = animal.AnimalTypeId,

                ShelterName = animal.Shelter?.Name ?? "",
                AnimalTypeName = animal.AnimalType?.Name ?? "",

                Name = animal.Name,
                DateOfBirth = animal.DateOfBirth,
                Age = animal.DateOfBirth.HasValue ? CalculateAge(animal.DateOfBirth.Value) : 0,
                Color = animal.Color,
                AdoptionFee = animal.AdoptionFee,
                Breed = animal.Breed,
                IsVaccinated = animal.IsVaccinated,
                HasSpecialCareNeeds = animal.HasSpecialCareNeeds,
                HasSpecialDiet = animal.HasSpecialDiet,
                IsAdopted = animal.IsAdopted,
                IsActive = animal.IsActive
            };
        }


        public async Task<AnimalViewModel?> GetAnimalUpdateAsync(int id)
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
            if (animal == null) return null;

            return new AnimalViewModel
            {
                Id = animal.Id,

                ShelterId = animal.ShelterId,
                AnimalTypeId = animal.AnimalTypeId,

                ShelterName = animal.Shelter?.Name ?? "",
                AnimalTypeName = animal.AnimalType?.Name ?? "",

                Name = animal.Name,
                DateOfBirth = animal.DateOfBirth,
                Age = animal.DateOfBirth.HasValue ? CalculateAge(animal.DateOfBirth.Value) : 0,
                Color = animal.Color,
                AdoptionFee = animal.AdoptionFee,
                Breed = animal.Breed,
                IsVaccinated = animal.IsVaccinated,
                HasSpecialCareNeeds = animal.HasSpecialCareNeeds,
                HasSpecialDiet = animal.HasSpecialDiet,
                IsAdopted = animal.IsAdopted
                
            };

        }




        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _context.Animals
                .AsNoTracking()
                .Where(a => a.IsActive)
                .ToListAsync();
        }

        public async Task<Animal?> GetByIdAsync(int id)
        {
            return await _context.Animals.FirstOrDefaultAsync(a  => a.Id == id);
        }

        public async Task<List<SelectListItem>> GetSelectListItemsAsync()
        {
            var animal = await GetAllAsync();

            return animal.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
        }
    }
}
