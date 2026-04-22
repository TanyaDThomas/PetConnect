namespace PetConnect.Domain.Entities
{
    public class Animal
    {
        public int Id { get; set; }
        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; } = null!;

        public int AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; } = null!;

        public string Name { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Color { get; set; } = "";
        public decimal AdoptionFee { get; set; }
        public string Breed { get; set; } = "";

        public bool IsVaccinated { get; set; }
        public bool HasSpecialCareNeeds { get; set; }
        public bool HasSpecialDiet { get; set; }
        public bool IsAdopted { get; set; }
        public bool IsActive { get; set; } = true;


        public ICollection<AnimalAttribute> AnimalAttributes { get; set; } = new List<AnimalAttribute>();

        public ICollection<Adoption> Adoptions { get; set; } = new List<Adoption>();

        // Accountability

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
