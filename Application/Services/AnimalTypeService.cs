using Microsoft.AspNetCore.Http.HttpResults;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;

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
