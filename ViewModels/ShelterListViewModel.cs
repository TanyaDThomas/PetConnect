using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace PetConnect.ViewModels
{
    public class ShelterListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

    }
}
