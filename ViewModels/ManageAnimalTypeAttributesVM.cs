using System.ComponentModel.DataAnnotations;


namespace PetConnect.ViewModels
{
    public class ManageAnimalTypeAttributesVM
    {
        public int AnimalTypeId { get; set; }

        public string AnimalTypeName { get; set; } = "";


        public List<AttributeCheckboxVM> Attributes { get; set; } = new List<AttributeCheckboxVM>();
    }
}
