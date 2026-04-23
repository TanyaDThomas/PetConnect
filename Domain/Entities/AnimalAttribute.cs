namespace PetConnect.Domain.Entities
{
    public class AnimalAttribute
    {

        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;
        public int AttributeDefinitionId { get; set; }
        public AttributeDefinition AttributeDefinition { get; set; } = null!;

        public string? Value { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
