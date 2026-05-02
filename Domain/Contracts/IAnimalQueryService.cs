using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Application.Services;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAnimalQueryService
    {
        Task<IEnumerable<Animal>> GetAllAsync();
        Task<Animal?> GetByIdAsync(int id);

        Task<AnimalIndexViewModel> GetAnimalListAsync(AnimalSearchFilter filter);
        Task<List<AnimalType>> GetAnimalTypesAsync();
        Task<AnimalViewModel?> GetAnimalDetailsAsync(int id);

        Task<AnimalViewModel?> GetAnimalUpdateAsync(int id);
        Task<List<SelectListItem>> GetSelectListItemsAsync();

        Task<decimal?> GetAnimalFeeAsync(int animalId);
    }
}
