namespace PetConnect.Domain.Entities
{
    public class Shelter
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";

        public bool IsActive { get; set; } = true;

        // Accountability
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();
        public ICollection<Adopter> Adopters { get; set; } = new List<Adopter>();
        public ICollection<Adoption> Adoptions { get; set; }= new List<Adoption>();
    }
}
