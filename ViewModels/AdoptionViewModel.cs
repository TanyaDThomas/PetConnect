using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetConnect.ViewModels
{
    public class AdoptionViewModel
    {
        public int Id { get; set; }

        /*--------------
         * -- SHELTER --
         ---------------*/
     
        [Range(1, int.MaxValue)]
        public int ShelterId { get; set; }
        public string ShelterName { get; set; } = "";
        public IEnumerable<SelectListItem> Shelters { get; set; } = new List<SelectListItem>();

        /*--------------
          -- ADOPTER --
        ---------------*/

        [Range(1, int.MaxValue)]
        public int AdopterId { get; set; }
        public string AdopterName { get; set; } = "";
        public IEnumerable<SelectListItem> Adopters { get; set; } = new List<SelectListItem>();


        /*--------------
           -- ANIMAL --
        ---------------*/
        [Range(1, int.MaxValue)]
        public int AnimalId { get; set; }
        public string AnimalName { get; set; } = "";
        public IEnumerable<SelectListItem> Animals { get; set; } = new List<SelectListItem>();


        /*---------------------
          -- ADOPTION INFO  --
        ----------------------*/
        public AdoptionStatus Status { get; set; } 

        [Range(0, 10000)]
        public decimal AdoptionFee { get; set; }

        /*---------------------
       -- DISPLAY INFO  --
      ----------------------*/
        public string FullName => $"{AdopterName}";


    }
}
