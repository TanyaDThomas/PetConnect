namespace PetConnect.Application.Services
{
    public class AdopterSearchFilter
    {

        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? ActiveOnly { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public bool? HasChildren { get; set; }
        public bool? HasOtherPets { get; set; }
    }
}
