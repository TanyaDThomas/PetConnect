using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAnimalTypeService
    {
        Task<bool> CreateAsync(AnimalType animalType);
        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAttributesAsync(ManageAnimalTypeAttributesVM viewModel);
    }
}
