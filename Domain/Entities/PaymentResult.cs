using PetConnect.Domain.Enums;

namespace PetConnect.Domain.Entities
{
    public class PaymentResult
    {
        
        public bool Success { get; set; }

        public PaymentStatus Status { get; set; }

        // Transaction/token ID from processor
        public string? TransactionId { get; set; }

        // Optional: Message for debugging or display
        public string? Message { get; set; }
    }
}
