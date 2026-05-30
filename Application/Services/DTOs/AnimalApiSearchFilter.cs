namespace PetConnect.Application.Services.DTOs
{
    public class AnimalApiSearchFilter
    {
        
        public string? Name { get; set; }
        public string? Breed { get; set; }
        public int? Age { get; set; }

        public string? City { get; set; }
        public string? State { get; set; }
        public int? AnimalTypeId { get; set; }

       

        public string? ShelterName { get; set; }
    
        public bool? HasSpecialCareNeeds { get; set; }


    }
}
