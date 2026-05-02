using PetConnect.Domain.Entities;

namespace PetConnect.Application.Services
{
    public class AnimalSearchFilter
    {
        public string? Name { get; set; }

        public int? AnimalTypeId { get; set; }
       
        public bool? IsAvailable { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public bool? IsGoodWithChildren { get; set; }
        public bool? IsGoodWithOtherPets { get; set; }
        public bool? HasSpecialCareNeeds { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
    }
}
