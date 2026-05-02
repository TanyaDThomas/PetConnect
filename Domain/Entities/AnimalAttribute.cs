namespace PetConnect.Domain.Entities
{
    public class AnimalAttribute
    {
      
            public int AnimalId { get; set; }

            public int AttributeDefinitionId { get; set; }

            public string? Value { get; set; }

            public bool IsActive { get; set; } = true;

            public AttributeDefinition AttributeDefinition { get; set; } = null!;

       
    }
}
