using Microsoft.AspNetCore.Mvc;

namespace PetConnect.Domain.Contracts
{
    public interface IUserShelterService
    {
        Task<bool> CreateAsync(string userId, int shelterId, string roleInShelter);
        Task<bool> UpdateAsync(string userId, int shelterId, string roleInShelter);
        Task<bool> DeactivateAsync(string userId, int shelterId);


    }
}
