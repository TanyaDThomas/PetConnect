namespace PetConnect.ViewModels
{
    public class AdopterIndexViewModel
    {
        public IEnumerable<AdopterListViewModel> Adopters { get; set; }

        public int FilteredCount { get; set; }

        public int TotalCount { get; set; }
    }
}
