namespace PetConnect.Domain.Contracts
{
    public interface IShelterAuthorizationService
    {
       
        Task<bool> CanAccessShelterAsync(string userId, int shelterId);  // Shelter membership check
        Task<bool> CanManageShelterAsync(string userId, int shelterId); // CRUD opertaions within assigned shelter 
        Task<bool> IsAdminAsync(string userId); // Admin check (system-wide)
    }
}
