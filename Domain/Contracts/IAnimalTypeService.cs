using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface IAnimalTypeService
    {
        Task<bool> CreateAsync(AnimalType animalType);
        Task<bool> DeleteAsync(int id);
    }
}
