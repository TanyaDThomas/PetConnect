using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Application.Services
{
    public class ShelterService : IShelterService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<ShelterService> _logger;
        public ShelterService(PetConnectDbContext context, ILogger<ShelterService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(ShelterViewModel viewModel)
        {
            try
            {
                var shelter = new Shelter()
                {
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    City = viewModel.City,
                    State = viewModel.State,
                    PostalCode = viewModel.PostalCode,
                    PhoneNumber = viewModel.PhoneNumber,
                    Email = viewModel.Email
                };

                shelter.CreatedOn = DateTime.UtcNow;
                shelter.CreatedBy = "System";
                shelter.IsActive = true;

                _context.Shelters.Add(shelter);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Shelter created with Id {ShelterId}",
                    shelter.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Opertaion failed. Shelter not created.");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(ShelterViewModel viewModel)
        {
            try
            {
                var shelter = await _context.Shelters.FindAsync(viewModel.Id);
                if (shelter == null) return false;

                shelter.Id = viewModel.Id;
                shelter.Name = viewModel.Name;
                shelter.Address = viewModel.Address;
                shelter.City = viewModel.City;
                shelter.State = viewModel.State;
                shelter.PostalCode = viewModel.PostalCode;
                shelter.PhoneNumber = viewModel.PhoneNumber;
                shelter.Email = viewModel.Email;

                shelter.UpdatedOn = DateTime.UtcNow;
                shelter.UpdatedBy = "System";

                _context.Shelters.Update(shelter);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Shelter updated with id {ShelterId}",
                    shelter.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Shelter was not updated.");
                return false;
            }
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            try
            {
                var shelter = await _context.Shelters.FindAsync(id);
                if (shelter == null) return false;

                shelter.UpdatedOn = DateTime.UtcNow;
                shelter.UpdatedBy = "System";

                shelter.IsActive = false;
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogWarning("Shelter deactivate by Id {ShelterId}",
                    id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Shelter was not deactivated.");
                return false;
            }
        }

       
    }
}
