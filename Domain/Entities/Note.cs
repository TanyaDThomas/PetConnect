using PetConnect.Domain.Enums;

namespace PetConnect.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        
        public NoteEntityType EntityType { get; set; }  // "Adopter" or "Animal"
        public int EntityId { get; set; }

        public NoteCategory Category { get; set; }

        public string Content { get; set; } = "";

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }

        public bool IsInternal { get; set; } = false;

        public string DisplayTitle => $"{Category} Note ({CreatedOn:MM/dd/yyyy})";


        // Staff member accountability
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
