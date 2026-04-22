using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<AnimalService> _logger;
        public AnimalService(PetConnectDbContext context, ILogger<AnimalService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(AnimalViewModel viewModel)
        {
            try
            {
                var animal = new Animal
                {
                    Id = viewModel.Id,
                    ShelterId = viewModel.ShelterId,
                    AnimalTypeId = viewModel.AnimalTypeId,
                    Name = viewModel.Name,
                    DateOfBirth = viewModel.DateOfBirth,
                    Breed = viewModel.Breed,
                    Color = viewModel.Color,
                    AdoptionFee = viewModel.AdoptionFee,
                    IsVaccinated = viewModel.IsVaccinated,
                    HasSpecialCareNeeds = viewModel.HasSpecialCareNeeds,
                    HasSpecialDiet = viewModel.HasSpecialDiet

                };

                animal.CreatedOn = DateTime.UtcNow;
                animal.CreatedBy = "System";
                animal.IsActive = true;
                animal.IsAdopted = false;

                _context.Animals.Add(animal);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Animal created with Id {AnimalId}",
                    animal.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Animal was not created");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(AnimalViewModel viewModel)
        {
            try
            {
                var animal = new Animal
                {
                    Id = viewModel.Id,
                    ShelterId = viewModel.ShelterId,
                    AnimalTypeId = viewModel.AnimalTypeId,
                    Name = viewModel.Name,
                    DateOfBirth = viewModel.DateOfBirth,
                    Breed = viewModel.Breed,
                    Color = viewModel.Color,
                    AdoptionFee = viewModel.AdoptionFee,
                    IsVaccinated = viewModel.IsVaccinated,
                    HasSpecialCareNeeds = viewModel.HasSpecialCareNeeds,
                    HasSpecialDiet = viewModel.HasSpecialDiet,
                    IsActive = viewModel.IsActive,
                    IsAdopted = viewModel.IsAdopted
                };

                animal.UpdatedOn = DateTime.UtcNow;
                animal.UpdatedBy = "System";

                _context.Animals.Update(animal);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Animal updated with Id {AnimalId}",
                    animal.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Animal not updated.");
                return false;
            }
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            try
            {
                var existingAnimal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
                if (existingAnimal == null) return false;

                existingAnimal.UpdatedOn = DateTime.UtcNow;
                existingAnimal.UpdatedBy = "System";

                existingAnimal.IsActive = false;
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Animal Deactivated with Id {AnimalId}",
                    existingAnimal.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Animal not deactivated");
                return false;
            }
        }

    }
}
