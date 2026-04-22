using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAdopterService
    {
       Task<bool> CreateAsync(AdopterViewModel viewModel);
       Task<bool> UpdateAsync(AdopterViewModel viewModel);
        Task<bool> DeactivateAsync(int id);
    }
}
