namespace PetConnect.Domain.Entities
{
    public class AnimalImage
    {
        public int Id { get; set; }
        public string FileName { get; set; } = "";
        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = new();
    }
}
