using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IShelterQueryService
    {
        Task<IEnumerable<Shelter>> GetAllAsync();
        Task<Shelter?> GetByIdAsync(int id);

        Task<List<SelectListItem>> GetSelectListItemsAsync();
        Task<List<ShelterListViewModel>> GetShelterListAsync();

        Task<ShelterViewModel?> GetShelterDetailsAsync(int id);
        Task<ShelterViewModel?> GetShelterUpdateAsync(int id);

        Task<List<Shelter>> GetSheltersForManagerAsync(string userId);

        Task<List<Shelter>> GetSheltersForDropdownAsync();


    }
}
