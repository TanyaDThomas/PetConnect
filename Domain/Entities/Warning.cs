using PetConnect.Domain.Enums;

namespace PetConnect.Domain.Entities
{
    public class Warning
    {
        public int Id { get; set; }

        public WarningEntityType EntityType { get; set; }
        public int EntityId { get; set; }

        public WarningSeverity Severity { get; set; }
        public WarningStatus Status { get; set; }

        public string Description { get; set; } = "";

        public bool IsActive { get; set; } = true;

        public bool BlocksAdoption { get; set; }

        // Accountability
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }

        public DateTime? ResolvedOn { get; set; }
        public string? ResolvedBy { get; set; }

    }
}
