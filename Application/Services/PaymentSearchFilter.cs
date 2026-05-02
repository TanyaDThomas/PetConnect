using PetConnect.Domain.Enums;

namespace PetConnect.Application.Services
{
    public class PaymentSearchFilter
    {
        public int? AdoptionId { get; set; }
        public int? AdopterId { get; set; }
        public PaymentType? PaymentType { get; set; }
        public PaymentStatus? Status { get; set; }


        public decimal? AmountMin { get; set; }
        public decimal? AmountMax { get; set; }


        public DateTime? PaymentDateFrom { get; set; }
        public DateTime? PaymentDateTo { get; set; }


        public bool? ActiveOnly { get; set; } = true;

        // Reporting
        public string? CreatedBy { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}
