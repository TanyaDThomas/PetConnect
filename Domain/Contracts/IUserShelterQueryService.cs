using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IUserShelterQueryService
    {
        Task<List<UserShelter>> GetUserShelterListAsync();

        Task<List<UserShelter>> GetAssignmentsForUserAsync(string userId);

        Task<UserShelter?> GetByIdAsync(int id);

        Task<UserShelter?> GetByUserAndShelterAsync(string userId, int shelterId);

  
    }
}
