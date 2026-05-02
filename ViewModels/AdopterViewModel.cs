using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetConnect.ViewModels
{
    public class AdopterViewModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a shelter")]
        public int ShelterId { get; set; }
        public string ShelterName { get; set; } = "";
        public List<SelectListItem> Shelters { get; set; } = new();


        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = "";

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = "";

        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [StringLength(75)]
        public string Address { get; set; } = "";

        [Required]
        [StringLength(75)]
        public string City { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string State { get; set; } = "";

        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; } = "";

        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = "";

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = "";

        //Household Information
        public bool HasOtherPets { get; set; }
        public bool HasChildren { get; set; }
        public bool HasYard { get; set; }



        // Notes
        public IEnumerable<Note> RecentNotes { get; set; } = new List<Note>();
        public string? ReturnUrl { get; set; }
        public NoteEntityType EntityType { get; set; } = NoteEntityType.Adopter;
    }
}
