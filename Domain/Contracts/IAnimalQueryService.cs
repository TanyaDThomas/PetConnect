using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAnimalQueryService
    {
        Task<IEnumerable<Animal>> GetAllAsync();
        Task<Animal?> GetByIdAsync(int id);
        Task<IEnumerable<AnimalListViewModel>> GetAnimalListAsync();
        Task<AnimalViewModel?> GetAnimalDetailsAsync(int id);

        Task<AnimalViewModel?> GetAnimalUpdateAsync(int id);
        Task<List<SelectListItem>> GetSelectListItemsAsync();

        Task<decimal?> GetAnimalFeeAsync(int animalId);
    }
}
