using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class AnimalDetailsViewModel
    {
        public string Name { get; set; } = "";
        public string Breed { get; set; } = "";
        public string Color { get; set; } = "";

        public List<AnimalAttributeVm> Attributes { get; set; } = new();

      
       
        
    }
}
