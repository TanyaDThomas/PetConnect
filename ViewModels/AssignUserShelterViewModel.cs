using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetConnect.ViewModels
{
    public class AssignUserShelterViewModel
    {
        //public int Id { get; set; }
        public string UserId { get; set; } = "";
      
        public int ShelterId { get; set; }

        public string City { get; set; } = "";
        public string State { get; set; } = "";

        public string RoleInShelter { get; set; } = "";

        public IEnumerable<SelectListItem> Users { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Shelters { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Roles { get; set; }
            = new List<SelectListItem>();
    }
}
