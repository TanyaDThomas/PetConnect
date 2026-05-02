using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAnimalTypeQueryService
    {
        Task<IEnumerable<AnimalType>> GetAllAsync();
        Task<IEnumerable<SelectListItem>> GetSelectListItemsAsync();

        Task<ManageAnimalTypeAttributesVM?> BuildManageAttributesModelAsync(int animalTypeId);

    }
}
