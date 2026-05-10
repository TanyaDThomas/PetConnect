using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAdopterService
    {
       Task<bool> CreateAsync(AdopterViewModel viewModel, string userName);
       Task<bool> UpdateAsync(AdopterViewModel viewModel, string userName);
        Task<bool> DeactivateAsync(int id, string userName);
    }
}
