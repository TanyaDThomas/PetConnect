using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;

namespace PetConnect.Application.Services
{
    public class AdoptionSearchFilter
    {
        public string? AdopterName { get; set; }
        public int? AdopterId { get; set; }
        public int? AnimalId { get; set; }
        public AnimalType? AnimalType { get; set; }

      
        public DateTime? AdoptionDateFrom { get; set; }
        public DateTime? AdoptionDateTo { get; set; }

       
        public AdoptionStatus? Status { get; set; }

      
        public bool? ActiveOnly { get; set; } = true;

       
        public string? CreatedBy { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }


    }
}
