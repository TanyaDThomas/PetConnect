using Microsoft.AspNetCore.Identity;
using PetConnect.Domain.Entities;

namespace PetConnect.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName => $"{FirstName} {LastName}";

        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string PostalCode { get; set; } = "";

        public ICollection<UserShelter> UserShelters { get; set; } = new List<UserShelter>();

    }
}
