using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class PaymentDetailsViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public PaymentType Type { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }

        public int? AdoptionId { get; set; }
        public int? AdopterId { get; set; }


        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        public string? LastFourDigits { get; set; }
        public string? BankName { get; set; }
        public string? CheckNumber { get; set; }
        public string? PaypalEmail { get; set; }

        public string? ReceiptNumber { get; set; }
        public string? Notes { get; set; }
        public string? FailureReason { get; set; }

        // Notes
        public IEnumerable<Note> RecentNotes { get; set; } = new List<Note>();
        public string? ReturnUrl { get; set; }
        public NoteEntityType EntityType { get; set; } = NoteEntityType.Payment;
    }
}
