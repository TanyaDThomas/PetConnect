using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IShelterService
    {
        Task<bool> CreateAsync(ShelterViewModel viewModel, string userName);
        Task<bool> UpdateAsync(ShelterViewModel viewModel, string userName);
        Task<bool> DeactivateAsync(int id, string userName);
    }
}
