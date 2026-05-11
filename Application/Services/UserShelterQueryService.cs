using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Identity;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Application.Services
{
    public class UserShelterQueryService : IUserShelterQueryService
    {
        private readonly PetConnectDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserShelterQueryService(PetConnectDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserShelter?> GetByIdAsync(int id)
        {
            return await _context.UserShelters
                .AsSplitQuery()
                .Include(us => us.User)
                .Include(us => us.Shelter)
                .FirstOrDefaultAsync(us => us.Id == id);
            }


        /*////////////////
        //// IDENITY ////
        //////////////*/

        public async Task<List<UserShelter>> GetUserShelterListAsync()
        {
            return await _context.UserShelters
                .AsNoTracking()
                .AsSplitQuery()
                .Include(us => us.User)
                .Include(us => us.Shelter)
                .Where(us => us.IsActive)
                .ToListAsync();

        }


        public async Task<UserShelter?> GetByUserAndShelterAsync(string userId, int shelterId)
        {
            return await _context.UserShelters
                .Include(x => x.User)
                .Include(x => x.Shelter)
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.ShelterId == shelterId);
        }




        public async Task<List<UserShelter>> GetAssignmentsForUserAsync(string userId)
        {
            var shelterIds = await _context.UserShelters
                .Where(x => x.UserId == userId && x.IsActive)
                .Select(x => x.ShelterId)
                .ToListAsync();

            return await _context.UserShelters
                .AsNoTracking()
                .Include(us => us.User)
                .Include(us => us.Shelter)
                .Where(us =>
                    us.IsActive &&
                    shelterIds.Contains(us.ShelterId))
                .ToListAsync();
        }

    
    }
}
