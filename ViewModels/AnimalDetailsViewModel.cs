using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class AnimalDetailsViewModel
    {
        public int Id { get; set; }

        public string? ImagePath { get; set; }

        public string Name { get; set; } = "";
        public string AnimalTypeName { get; set; } = "";
        public int AnimalTypeId { get; set; }
        public int ShelterId { get; set; }
        public string ShelterName { get; set; } = "";

     

        public string Breed { get; set; } = "";
        public string Color { get; set; } = "";

        public decimal AdoptionFee { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }

        public bool IsVaccinated { get; set; }
        public bool HasSpecialCareNeeds { get; set; }
        public bool HasSpecialDiet { get; set; }
        public bool IsAdopted { get; set; }
        public bool IsActive { get; set; }
 

        public List<AnimalImage> Images { get; set; } = new();

        public List<AnimalAttributeVm> Attributes { get; set; } = new();

        public IEnumerable<Note> RecentNotes { get; set; } = new List<Note>();

        public NoteEntityType EntityType { get; set; } = NoteEntityType.Animal;

    }
}
