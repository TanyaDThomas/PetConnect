using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAnimalService
    {
        Task<bool> CreateAsync(AnimalViewModel viewModel, string userName);
        Task<bool> UpdateAsync(AnimalViewModel viewModel, string userName);
        Task<bool> DeactivateAsync(int id, string userName);

        Task AddImagesAsync(int animalId, List<IFormFile> files);
        Task DeleteImageAsync(int imageId);
       
        Task SetProfileImageAsync(int animalId, int imageId);

    }
}
