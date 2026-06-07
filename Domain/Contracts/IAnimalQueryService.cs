using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Application.Services.DTOs;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IAnimalQueryService
    {
        Task<IEnumerable<Animal>> GetAllAsync();
        Task<Animal?> GetByIdAsync(int id);

        Task<AnimalIndexViewModel> GetAnimalListAsync(AnimalSearchFilter filter, string userId);
        Task<List<AnimalType>> GetAnimalTypesAsync();
        Task<AnimalDetailsViewModel?> GetAnimalDetailsAsync(int id);

        Task<AnimalViewModel?> GetAnimalUpdateAsync(int id);
        Task<List<SelectListItem>> GetSelectListItemsAsync();

        Task<decimal?> GetAnimalFeeAsync(int animalId);

        Task<List<SelectListItem>> GetSelectListBySheltersAsync(List<int> shelterIds);


        Task<IEnumerable<AnimalDto>> ApiSearchAsync(AnimalApiSearchFilter filter);
        Task<AnimalDetailsDto?> GetByIdApiAsync(int id);



    }
}
