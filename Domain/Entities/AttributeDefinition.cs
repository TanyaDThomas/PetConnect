using PetConnect.Domain.Enums;

namespace PetConnect.Domain.Entities
{
    public class AttributeDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public AttributeDataType DataType { get; set; }
        public ICollection<AnimalAttribute> AnimalAttributes { get; set; } = new List<AnimalAttribute>();

        public ICollection<AnimalTypeAttribute> AnimalTypeAttributes { get; set; } = new List<AnimalTypeAttribute>();
    }
}
