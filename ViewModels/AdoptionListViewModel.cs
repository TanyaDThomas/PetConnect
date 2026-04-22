using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class AdoptionListViewModel
    {
        public int Id { get; set; }
        public int ShelterId { get; set; }
        public string ShelterName { get; set; } = "";

        public int AdopterId { get; set; }
        public string AdopterName { get; set; } = "";

        public string AnimalType { get; set; } = "";

        public int AnimalId { get; set; }
        public string AnimalName { get; set; } = "";

        public DateTime AdoptionDate { get; set; }
        public AdoptionStatus Status { get; set; }
    }
}
