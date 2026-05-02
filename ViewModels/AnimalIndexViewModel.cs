using PetConnect.Application.Services;
using PetConnect.Domain.Entities;

namespace PetConnect.ViewModels
{
    public class AnimalIndexViewModel
    {
     
        public List<AnimalListViewModel> Animals { get; set; } = new();
        public AnimalSearchFilter Filter { get; set; } = new();
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public List<AnimalType> AnimalTypes { get; set; } = new();
    }
}
