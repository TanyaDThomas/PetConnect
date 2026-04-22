using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class AdoptionDetailsViewModel
    {
        public int Id { get; set; }

        public string ShelterName { get; set; } = "";
        public string AdopterName { get; set; } = "";
        public string AnimalType { get; set; } = "";
        public string AnimalName { get; set; } = "";

        public decimal AdoptionFee { get; set; }
        public DateTime AdoptionDate { get; set; }
        public AdoptionStatus Status { get; set; }


    }
}
