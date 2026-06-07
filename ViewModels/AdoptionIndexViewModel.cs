using PetConnect.Application.Services;
using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class AdoptionIndexViewModel
    {
        public IEnumerable<AdoptionListViewModel> Adoptions { get; set; } = new List<AdoptionListViewModel>();
        public AdoptionSearchFilter Filter { get; set; }  = new();

        public int FilteredCount { get; set; }
        public int TotalCount { get; set; }

        public AdoptionStatus? Status { get; set; }

    }
}
