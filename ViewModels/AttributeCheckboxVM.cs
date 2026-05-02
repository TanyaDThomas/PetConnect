using System.ComponentModel.DataAnnotations;

namespace PetConnect.ViewModels
{
    public class AttributeCheckboxVM
    {
        public int AttributeDefinitionId { get; set; }

        public string? Value { get; set; }
        public string Name { get; set; } = "";
        public bool IsSelected { get; set; }
    }
}
