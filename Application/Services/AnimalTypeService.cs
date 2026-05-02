using Microsoft.AspNetCore.Http.HttpResults;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Application.Services
{
    public class AnimalTypeService : IAnimalTypeService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<AnimalTypeService> _logger;
        public AnimalTypeService(PetConnectDbContext context, ILogger<AnimalTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(AnimalType animalType)
        {
            try
            {
                _context.Add(animalType);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("AnimalType created with Id {AnimalTypeId}",
                    animalType.Id);

                return rowsAffected > 0;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. AnimalType not created");
                return false;
            }
        }

        //UPDATE MANAGE ATTRIBUTE 
        public async Task<bool> UpdateAttributesAsync(ManageAnimalTypeAttributesVM viewModel)
        {
            try
            {
                var existingAttribute = _context.AnimalTypeAttributes.Where(at => at.AnimalTypeId == viewModel.AnimalTypeId);

                _context.AnimalTypeAttributes.RemoveRange(existingAttribute);

                var selected = viewModel.Attributes
                   .Where(a => a.IsSelected)
                   .Select(a => new AnimalTypeAttribute
                   {
                       AnimalTypeId = viewModel.AnimalTypeId,
                       AttributeDefinitionId = a.AttributeDefinitionId
                   })
                   .ToList();

                await _context.AnimalTypeAttributes.AddRangeAsync(selected);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Animal Type Attributes updated with Id {AnimalTypeId}",
                   viewModel.AnimalTypeId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Failed to update attributes for AnimalType {AnimalTypeId}",
                    viewModel.AnimalTypeId);

                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var animalType = await _context.AnimalTypes.FindAsync(id);
                if (animalType == null) return false;

                _context.AnimalTypes.Remove(animalType);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogWarning("AnimalType deleted with Id {AnimalTypeId}",
                    id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. AnimalType not deleted.");
                return false;
            }
        }
    }
}
