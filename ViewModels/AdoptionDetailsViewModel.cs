using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class AdoptionDetailsViewModel
    {
        public int Id { get; set; }

        public string ShelterName { get; set; } = "";
        public string AdopterName { get; set; } = "";
        public string AnimalType { get; set; } = "";
        public string AnimalName { get; set; } = "";

        public decimal AdoptionFee { get; set; }
        public DateTime AdoptionDate { get; set; }
        public AdoptionStatus Status { get; set; }

        public PaymentStatus? PaymentStatus { get; set; }

        //Notes
   
        public IEnumerable<Note> RecentNotes { get; set; } = new List<Note>();
        public string? ReturnUrl { get; set; }
        public NoteEntityType EntityType { get; set; } = NoteEntityType.Adoption;
    }
}
