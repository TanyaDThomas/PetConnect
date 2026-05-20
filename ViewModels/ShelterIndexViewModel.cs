using PetConnect.Application.Services;

namespace PetConnect.ViewModels
{
    public class ShelterIndexViewModel
    {
        public IEnumerable<ShelterListViewModel> Shelters { get; set; } = new List<ShelterListViewModel>();
        public int FilteredCount { get; set; }
        public int TotalCount { get; set; }
    }
}
