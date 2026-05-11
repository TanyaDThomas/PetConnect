using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Identity;
using PetConnect.Infrastructure.Persistence;

namespace PetConnect.Application.Services
{
    public class UserShelterService : IUserShelterService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<UserShelterService> _logger;
     
        private readonly UserManager<AppUser> _userManager;

        public UserShelterService(PetConnectDbContext context, ILogger<UserShelterService> logger,  UserManager<AppUser> userManager)
        {
            _context = context;
            _logger = logger;
         
            _userManager = userManager;
        }


        public async Task<bool> CreateAsync(string userId, int shelterId, string roleInShelter)
        {
            var assignment = await _context.UserShelters
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ShelterId == shelterId);

            if (assignment != null)
            {
                assignment.IsActive = true;
                assignment.RoleInShelter = roleInShelter;
            }
            else
            {
                _context.UserShelters.Add(new UserShelter
                {
                    UserId = userId,
                    ShelterId = shelterId,
                    RoleInShelter = roleInShelter,
                    IsActive = true

                });
            }

            await _context.SaveChangesAsync();

      
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRoleAsync(user, roleInShelter);
            }

            return true;
        }

  



        public async Task<bool> UpdateAsync(string userId, int shelterId, string roleInShelter)
        {
            var existing = await _context.UserShelters
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.ShelterId == shelterId);

            if (existing == null)
                return false;

            existing.RoleInShelter = roleInShelter;

            await _context.SaveChangesAsync();
            return true;
        }


     



        public async Task<bool> DeactivateAsync(string userId, int shelterId)
        {
            var entity = await _context.UserShelters
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.ShelterId == shelterId);

            if (entity == null)
                return false;

            entity.IsActive = false;

            await _context.SaveChangesAsync();
            return true;
        }


    }
}
