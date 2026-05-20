using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAdopterQueryService 
    {
        Task<IEnumerable<Adopter>> GetAllAsync();
        Task<Adopter?> GetByIdAsync(int id);
        Task<List<AdopterListViewModel>> GetAdopterListAsync(string userId);
        Task<AdopterViewModel?> GetAdopterForUpdateAsync(int id);

        Task<AdopterViewModel?> GetAdopterDetailsAsync(int id);

        Task<List<SelectListItem>> GetSelectListItemsAsync();

        Task<List<SelectListItem>> GetSelectListBySheltersAsync(List<int> shelterIds);
    }

}
