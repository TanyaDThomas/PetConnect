namespace PetConnect.Domain.Contracts
{
    public interface IUserShelterService
    {
        Task<bool> CreateAsync(string userId, int shelterId, string roleInShelter);
        Task<bool> UpdateAsync(int id, string roleInShelter);
        Task<bool> DeactivateAsync(int id);

    }
}
