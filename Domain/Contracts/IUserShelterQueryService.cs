using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IUserShelterQueryService
    {
       
        Task<List<UserShelter>> GetUserShelterListAsync();
        Task<UserShelter?> GetByIdAsync(int id);

        Task<List<UserShelter>> GetAssignmentsForUserAsync(string userId);
    }
}
