using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IShelterQueryService
    {
        Task<IEnumerable<Shelter>> GetAllAsync();
        Task<Shelter?> GetByIdAsync(int id);

        Task<List<SelectListItem>> GetSelectListItemsAsync();
        Task<IEnumerable<ShelterListViewModel>> GetShelterListAsync();

        Task<ShelterViewModel?> GetShelterDetailsAsync(int id);
        Task<ShelterViewModel?> GetShelterUpdateAsync(int id);
    }
}
