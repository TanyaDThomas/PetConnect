using PetConnect.Domain.Enums;

namespace PetConnect.Domain.Entities
{
    public class Adoption
    {
        public int Id { get; set; }
        public int ShelterId { get; set; } 
        public Shelter Shelter { get; set; } = null!;

        public int AdopterId { get; set; }
        public Adopter Adopter { get; set; } = null!;

        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        public DateTime AdoptionDate { get; set; }
        public AdoptionStatus Status { get; set; }
        public decimal AdoptionFee { get; set; }

        public bool IsActive { get; set; } = true;

        // Accountability
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
