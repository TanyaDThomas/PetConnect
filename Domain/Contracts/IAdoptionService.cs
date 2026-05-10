using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAdoptionService
    {
        Task<bool> CreateAsync(AdoptionViewModel viewModel, string userName);
        Task<bool> UpdateAsync(AdoptionViewModel viewModel, string userName);
        Task<bool> DeactivateAsync(int id, string userName);
    }
}
