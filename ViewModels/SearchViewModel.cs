using PetConnect.Domain.Entities;

namespace PetConnect.ViewModels
{
    public class SearchViewModel
    {

        public string Query { get; set; } = "";

        public List<Animal> Animals { get; set; } = new();
        public List<Adopter> Adopters { get; set; } = new();
        public List<Adoption> Adoptions { get; set; } = new();
    }
}
