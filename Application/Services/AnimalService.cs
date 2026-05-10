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
        public async Task<bool> CreateAsync(AnimalViewModel viewModel, string userId)
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
                animal.CreatedBy = userId;
                animal.IsActive = true;
                animal.IsAdopted = false;

                _context.Animals.Add(animal);
                var rowsAffected = await _context.SaveChangesAsync();

                var attributeDefinitions = await _context.AnimalTypeAttributes
                 .Where(at => at.AnimalTypeId == animal.AnimalTypeId)
                 .Select(at => at.AttributeDefinition)
                 .ToListAsync();

                var animalAttributes = attributeDefinitions.Select(at =>
                    new AnimalAttribute
                    {
                        AnimalId = animal.Id,
                        AttributeDefinitionId = at.Id,
                        Value = null,
                        IsActive = true
                    }).ToList();

                _context.AnimalAttributes.AddRange(animalAttributes);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Animal created with Id {AnimalId}", animal.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Animal was not created");
                return false;
            }
        }



        public async Task<bool> UpdateAsync(AnimalViewModel viewModel, string userId)
        {
            try
            {
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == viewModel.Id);

                if (animal == null) return false;

               
                animal.ShelterId = viewModel.ShelterId;
                animal.AnimalTypeId = viewModel.AnimalTypeId;
                animal.Name = viewModel.Name;
                animal.DateOfBirth = viewModel.DateOfBirth;
                animal.Breed = viewModel.Breed;
                animal.Color = viewModel.Color;
                animal.AdoptionFee = viewModel.AdoptionFee;
                animal.IsVaccinated = viewModel.IsVaccinated;
                animal.HasSpecialCareNeeds = viewModel.HasSpecialCareNeeds;
                animal.HasSpecialDiet = viewModel.HasSpecialDiet;
                animal.IsAdopted = viewModel.IsAdopted;

                animal.UpdatedOn = DateTime.UtcNow;
                animal.UpdatedBy = userId;

                
                var existingAttributes = await _context.AnimalAttributes
                    .Where(a => a.AnimalId == viewModel.Id)
                    .ToListAsync();

                _context.AnimalAttributes.RemoveRange(existingAttributes);

                var newAttributes = viewModel.Attributes
                    .Where(a => !string.IsNullOrWhiteSpace(a.Value))
                    .Select(a => new AnimalAttribute
                    {
                        AnimalId = viewModel.Id,
                        AttributeDefinitionId = a.AttributeDefinitionId,
                        Value = a.Value
                    });

                await _context.AnimalAttributes.AddRangeAsync(newAttributes);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

    


        public async Task<bool> DeactivateAsync(int id, string userId)
        {
            try
            {
                var existingAnimal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
                if (existingAnimal == null) return false;

                existingAnimal.UpdatedOn = DateTime.UtcNow;
                existingAnimal.UpdatedBy = userId;

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
