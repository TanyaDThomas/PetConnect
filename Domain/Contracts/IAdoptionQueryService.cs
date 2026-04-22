using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAdoptionQueryService
    {
        Task<IEnumerable<Adoption>> GetAllAsync();
        Task<Adoption?> GetByIdAsync(int id);

        Task<List<AdoptionListViewModel>> GetAdoptionListAsync();
        Task<AdoptionViewModel?> GetAdoptionForUpdateAsync(int id);
        Task<AdoptionDetailsViewModel?> GetAdoptionDetailsAsync(int id);

       
    }
}
