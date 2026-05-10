using PetConnect.Infrastructure.Identity;
using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Entities
{
    public class UserShelter
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;

        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; } = null!;

        public string RoleInShelter { get; set; } = "Staff";

        public bool IsActive { get; set; } = true;
    }
}
