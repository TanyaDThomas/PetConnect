namespace PetConnect.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalAnimals { get; set;  }
        public int Adoptions { get; set; }

        public decimal TotalRevenue { get; set; }
        public int PendingAdoptions { get; set; }

        public List<string> SpeciesLabels { get; set; } = new();
        public List<int> SpeciesData { get; set; } = new();

        public List<string> AnimalTrendLabels { get; set; } = new();
        public List<int> AnimalTrendData { get; set; } = new();

     

    }
}
