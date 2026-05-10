using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;

namespace PetConnect.Application.Services
{
    public class UserShelterService : IUserShelterService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<UserShelterService> _logger;
        private readonly IUserShelterQueryService _queryService;

        public UserShelterService(PetConnectDbContext context, ILogger<UserShelterService> logger, IUserShelterQueryService queryService    )
        {
            _context = context;
            _logger = logger;
            _queryService = queryService;
        }

        public async Task<bool> CreateAsync(string userId, int shelterId, string roleInShelter)
        {
            var assignment = await _context.UserShelters
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.ShelterId == shelterId);

            if (assignment != null)
            {
                assignment.IsActive = true;
                assignment.RoleInShelter = roleInShelter;
                await _context.SaveChangesAsync();
                return true;
            }

            var userShelter = new UserShelter
            {
                UserId = userId,
                ShelterId = shelterId,
                RoleInShelter = roleInShelter,
                IsActive = true
            };

            _context.UserShelters.Add(userShelter);
            await _context.SaveChangesAsync();

            return true;
        }

        

        public async Task<bool> UpdateAsync(int id, string roleInShelter)
        {
            try
            {
                var existingUser = await _context.UserShelters.FirstOrDefaultAsync(eu => eu.Id == id);
                if (existingUser == null) return false;

                existingUser.RoleInShelter = roleInShelter;

                
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Updated UserShelter: {UserShelterId} {RoleInShelter}", id, roleInShelter);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserShelter update failed for Id {Id}", id);
                return false;
            }
       

        }

        public async Task<bool> DeactivateAsync(int id)
        {
            try
            {
                var userShelter = await _context.UserShelters.FirstOrDefaultAsync(us => us.Id == id);
                if(userShelter == null) return false;

                _logger.LogWarning("Deleting User in Shelter with Id {Id}", id);

                userShelter.IsActive = false;
                var rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User in Shelter not deleted with id {Id}", id);
                return false;
            }
        }


   
    }
}
