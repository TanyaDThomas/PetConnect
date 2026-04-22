using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface IWarningService
    {
        Task<Warning> CreateAsync(Warning warning);
        Task<bool> UpdateAsync(Warning warning);
        Task<bool> DeactivateAsync(int id);
    }
}
