using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Identity;
using PetConnect.Infrastructure.Persistence;

namespace PetConnect.Domain.Contracts
{
    public class ShelterAuthorizationService : IShelterAuthorizationService
    {
        private readonly PetConnectDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ShelterAuthorizationService(PetConnectDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> CanAccessShelterAsync(string userId, int shelterId)
        {
            return await _context.UserShelters.AnyAsync(a => a.UserId == userId && a.ShelterId == shelterId);
        }

        public async Task<bool> CanManageShelterAsync(string userId, int shelterId)
        {
            var membership = await _context.UserShelters.FirstOrDefaultAsync(m => m.UserId == userId);
            if (membership == null) return false;

            if(membership.RoleInShelter == ShelterRoles.Staff) 
                return false;

            return true;

        }

        public async Task<bool> IsAdminAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            return await _userManager.IsInRoleAsync(user, "Admin");
        }
    }
}
