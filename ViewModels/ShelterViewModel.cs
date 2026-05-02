using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace PetConnect.ViewModels
{
    public class ShelterViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

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

        [Required]
        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = "";

        public bool IsActive { get; set; }


        //Notes
        public IEnumerable<Note> RecentNotes { get; set; } = new List<Note>();
        public string? ReturnUrl { get; set; }
        public NoteEntityType EntityType { get; set; } = NoteEntityType.Shelter;

    }

}
