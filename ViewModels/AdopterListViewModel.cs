using PetConnect.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PetConnect.ViewModels
{
    public class AdopterListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public string ShelterName { get; set; } = "";

    }
}
