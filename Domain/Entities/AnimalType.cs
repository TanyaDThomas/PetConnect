namespace PetConnect.Domain.Entities
{
    public class AnimalType
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public ICollection<Animal> Animals { get; set; } = new List<Animal>();

        public ICollection<AnimalTypeAttribute> AnimalTypeAttributes { get; set; }
    = new List<AnimalTypeAttribute>();
    }
}
