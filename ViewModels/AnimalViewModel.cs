using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetConnect.ViewModels
{
    public class AnimalViewModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a shelter")]
        public int ShelterId { get; set; }
        public string? ShelterName { get; set; } = "";
        public IEnumerable<SelectListItem> Shelters { get; set; } = new List<SelectListItem>();

       
        [Range(1, int.MaxValue, ErrorMessage = "Please select an animal type")]
        public int AnimalTypeId { get; set; }

        public string? AnimalTypeName { get; set; } = "";
        public IEnumerable<SelectListItem> AnimalTypes { get; set; } = new List<SelectListItem>();

        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = "";

        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }


        [StringLength(100)]
        public string Breed { get; set; } = "";


        [StringLength(50)]
        public string Color { get; set; } = "";


        [Required(ErrorMessage = "Adoption fee is required")]
        [DataType(DataType.Currency)]
        [Range(0, 10000, ErrorMessage = "Adoption fee must be between 0 and 10,000")]
        public decimal AdoptionFee { get; set; }



        
        public bool IsVaccinated { get; set; }
        public bool HasSpecialCareNeeds { get; set; }
        public bool HasSpecialDiet { get; set; }
        public bool IsAdopted { get; set; }
        public bool IsActive { get; set; }

        public List<AnimalAttributeVm> Attributes { get; set; } = new List<AnimalAttributeVm>();


        public IEnumerable<Note> RecentNotes { get; set; } = new List<Note>();
        public string? ReturnUrl { get; set; }
        public NoteEntityType EntityType { get; set; } = NoteEntityType.Animal;
    }
}
