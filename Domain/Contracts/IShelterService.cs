using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IShelterService
    {
        Task<bool> CreateAsync(ShelterViewModel viewModel);
        Task<bool> UpdateAsync(ShelterViewModel viewModel);
        Task<bool> DeactivateAsync(int id);
    }
}
