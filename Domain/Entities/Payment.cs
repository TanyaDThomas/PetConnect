using PetConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetConnect.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public PaymentType Type { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public int? AdoptionId { get; set; } //Could be donation
        public Adoption? Adoption { get; set; }

        public int? AdopterId { get; set; }
        public Adopter? Adopter { get; set; }


        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PaymentToken { get; set; }
        public string? LastFourDigits { get; set; }
       
        public string? BankName { get; set; }
        public string? CheckNumber { get; set; }
        public string? PaypalEmail { get; set; }


        public string? ReceiptNumber { get; set; }
        public decimal RefundedAmount { get; set; } = 0;


        public string? Notes { get; set; }
        public string? FailureReason { get; set; }
        public bool IsActive { get; set; } = true;


        // For Accountability
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
