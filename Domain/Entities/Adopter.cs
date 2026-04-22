using System.ComponentModel.DataAnnotations;

namespace PetConnect.Domain.Entities
{
    public class Adopter
    {
        public int Id { get; set; }
        public int ShelterId { get; set; }
        public Shelter? Shelter { get; set; } = null!;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName => $"{FirstName} {LastName}";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";

        //Household Information
        public bool HasOtherPets { get; set; }
        public bool HasChildren { get; set; }
        public bool HasYard { get; set; }
        public bool IsActive { get; set; } = true;

        // Accountability
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }

        
        
        public ICollection<Adoption> Adoptions { get; set; } = new List<Adoption>();

    }
}
