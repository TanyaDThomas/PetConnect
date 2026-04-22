using PetConnect.Domain.Enums;

namespace PetConnect.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public NoteEntityType EntityType { get; set; }   // "Adopter" or "Animal"
        public int EntityId { get; set; }

        public NoteCategory Category { get; set; }
        public string Content { get; set; } = "";
        // For display
        public string DisplayTitle => $"{Category} Note ({CreatedOn:MM/dd/yyyy})";


        public bool IsInternal { get; set; } = false;
        public bool IsActive { get; set; } = true;


        // Accountability
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }

        
    }
}
