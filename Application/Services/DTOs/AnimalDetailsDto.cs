namespace PetConnect.Application.Services.DTOs
{
    public class AnimalDetailsDto
    {
        public int Id { get; set; }
        public int ShelterId { get; set; }
        public int AnimalTypeId { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }
        public decimal AdoptionFee { get; set; }
        public string Breed { get; set; }

        public bool IsVaccinated { get; set; }
        public bool HasSpecialCareNeeds { get; set; }
        public bool HasSpecialDiet { get; set; }
        public string? ImagePath { get; set; }
        public List<string> Images { get; set; }
    }
}
