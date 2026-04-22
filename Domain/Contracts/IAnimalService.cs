using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAnimalService
    {
        Task<bool> CreateAsync(AnimalViewModel viewModel);
        Task<bool> UpdateAsync(AnimalViewModel viewModel);
        Task<bool> DeactivateAsync(int id);
    }
}
