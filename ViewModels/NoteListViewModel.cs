using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class NoteListViewModel
    {
        public int Id { get; set; }
        public string EntityDisplayName { get; set; } = "";
        public NoteEntityType EntityType { get; set; }
        public NoteCategory Category { get; set; }
        public string ContentPreview { get; set; } = "";
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsInternal { get; set; }
    }
}
