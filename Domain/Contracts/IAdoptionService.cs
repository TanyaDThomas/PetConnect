using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAdoptionService
    {
        Task<bool> CreateAsync(AdoptionViewModel viewModel);
        Task<bool> UpdateAsync(AdoptionViewModel viewModel);
        Task<bool> DeactivateAsync(int id);
    }
}
