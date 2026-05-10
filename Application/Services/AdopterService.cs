
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Application.Services
{
    public class AdopterService : IAdopterService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<AdopterService> _logger;
        public AdopterService(PetConnectDbContext context, ILogger<AdopterService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(AdopterViewModel viewModel, string userId)
        {
            try
            {
                var adopter = new Adopter()
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Address = viewModel.Address,
                    City = viewModel.City,
                    State = viewModel.State,
                    PostalCode = viewModel.PostalCode,
                    PhoneNumber = viewModel.PhoneNumber,
                    Email = viewModel.Email,

                    HasChildren = viewModel.HasChildren,
                    HasOtherPets = viewModel.HasOtherPets,
                    HasYard = viewModel.HasYard,

                    ShelterId = viewModel.ShelterId,

                };

                adopter.CreatedOn = DateTime.UtcNow;
                adopter.CreatedBy = userId;
                adopter.IsActive = true;

                _context.Adopters.Add(adopter);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation(
                "Adopter created with Id {AdopterId}",
                adopter.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. No Adopter was created.");
                return false;
            }
           
        }

        
   
        public async Task<bool> UpdateAsync(AdopterViewModel viewModel, string userId)
        {
            try
            {
                var adopter = await _context.Adopters.FindAsync(viewModel.Id);

                if (adopter == null)
                    return false;


                adopter.FirstName = viewModel.FirstName;
                adopter.LastName = viewModel.LastName;
                adopter.Address = viewModel.Address;
                adopter.City = viewModel.City;
                adopter.State = viewModel.State;
                adopter.PostalCode = viewModel.PostalCode;
                adopter.PhoneNumber = viewModel.PhoneNumber;
                adopter.Email = viewModel.Email;
                adopter.HasChildren = viewModel.HasChildren;
                adopter.HasOtherPets = viewModel.HasOtherPets;
                adopter.HasYard = viewModel.HasYard;
                adopter.ShelterId = viewModel.ShelterId;

                

                adopter.UpdatedOn = DateTime.UtcNow;
                adopter.UpdatedBy = userId;


                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Adopter updated with Id {AdopterId}",
                    adopter.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Adopter not udpated.");
                return false;
            }
       
        }

        public async Task<bool> DeactivateAsync(int id, string userId)
        {
            try
            {
                var adopter = await _context.Adopters.FindAsync(id);
                if (adopter == null) return false;

                adopter.UpdatedOn = DateTime.UtcNow;
                adopter.UpdatedBy = userId;

                adopter.IsActive = false;
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogWarning(
                    "Adopter deactivated with Id {AdopterId}",
                    id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. No deactivation of Adopter occurred.");
                return false;
            }
        }

      
    }
}
