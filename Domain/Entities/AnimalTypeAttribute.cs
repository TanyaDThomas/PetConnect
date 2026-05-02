namespace PetConnect.Domain.Entities
{
    public class AnimalTypeAttribute
    {
        public int Id { get; set; }

        public int AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; } = null!;

        public int AttributeDefinitionId { get; set; }
        public AttributeDefinition AttributeDefinition { get; set; } = null!;

        public bool IsRequired { get; set; } = false;


    }
}
