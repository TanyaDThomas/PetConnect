using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PetConnect.Application.Services
{
    public class AdoptionService : IAdoptionService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<AdoptionService> _logger;
        public AdoptionService(PetConnectDbContext context, ILogger<AdoptionService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(AdoptionViewModel viewModel, string userId)
        {
           try
            {
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == viewModel.AnimalId);
                if (animal == null) return false;

                var adoption = new Adoption()
                {
                    Id = viewModel.Id,
                    ShelterId = viewModel.ShelterId,
                    AdopterId = viewModel.AdopterId,
                    AnimalId = viewModel.AnimalId,
                    Status = AdoptionStatus.Pending,
                    AdoptionFee = animal.AdoptionFee,
                };

                adoption.CreatedOn = DateTime.UtcNow;
                adoption.CreatedBy = userId;

                _context.Adoptions.Add(adoption);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Adoption created with Id {AdoptionId}",
                    adoption.Id);

                return rowsAffected > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Adoptin was not created.");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(AdoptionViewModel viewModel, string userId)
        {
            try
            {
                var adoption = await _context.Adoptions.FindAsync(viewModel.Id);
                if (adoption == null) return false;

                adoption.Id = viewModel.Id;
                adoption.ShelterId = viewModel.ShelterId;
                adoption.AdopterId = viewModel.AdopterId;
                adoption.AnimalId = viewModel.AnimalId;
                adoption.Status = viewModel.Status;
                adoption.AdoptionFee = viewModel.AdoptionFee;

                adoption.UpdatedOn = DateTime.UtcNow;
                adoption.UpdatedBy = userId;

                _context.Adoptions.Update(adoption);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Adoption updated with Id {AdoptionId}",
                    adoption.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Adoption not updated.");
                return false;
            }
        }
        public async Task<bool> DeactivateAsync(int id, string userId)
        {
            try
            {
                var adoption = _context.Adoptions.SingleOrDefault(a => a.Id == id);
                if (adoption == null) return false;

                adoption.UpdatedOn = DateTime.UtcNow;
                adoption.UpdatedBy= userId;

                adoption.IsActive = false;
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Adoption deactivated by Id {AdoptionId}",
                    adoption.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Adoption didn't deactivate.");
                return false;
            }
        }

      

     
    }
}
