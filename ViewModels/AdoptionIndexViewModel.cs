using PetConnect.Application.Services;

namespace PetConnect.ViewModels
{
    public class AdoptionIndexViewModel
    {
        public IEnumerable<AdoptionListViewModel> Adoptions { get; set; } = new List<AdoptionListViewModel>();
        public AdoptionSearchFilter Filter { get; set; }  = new();

        public int FilteredCount { get; set; }
        public int TotalCount { get; set; }

    }
}
