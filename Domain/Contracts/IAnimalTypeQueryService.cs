using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface IAnimalTypeQueryService
    {
        Task<IEnumerable<AnimalType>> GetAllAsync();
        Task<IEnumerable<SelectListItem>> GetSelectListItemsAsync();
    }
}
