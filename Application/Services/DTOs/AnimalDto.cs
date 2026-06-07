namespace PetConnect.Application.Services.DTOs
{
    public class AnimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AnimalTypeName { get; set; }
        public string Breed { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool HasSpecialCareNeeds { get; set; }
        public int Age { get; set; }
        public string? ImagePath { get; set; }   
        public List<string> Images { get; set; } = new();
    }
}
