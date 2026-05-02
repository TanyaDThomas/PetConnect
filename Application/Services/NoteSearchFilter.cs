using PetConnect.Domain.Enums;

namespace PetConnect.Application.Services
{
    public class NoteSearchFilter
    {
        public int? EntityId { get; set; }
        public bool? IsInternal { get; set; }

        public NoteCategory? Category { get; set; }
        public NoteEntityType? EntityType { get; set; }

        // Created / updated info
        public string? CreatedBy { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }

        public bool? ActiveOnly { get; set; } = true;
    }
}
