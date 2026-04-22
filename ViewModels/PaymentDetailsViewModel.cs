namespace PetConnect.ViewModels
{
    public class PaymentDetailsViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "";
        public string Type { get; set; } = "";
        public string PaymentDate { get; set; } = "";

        public int? AdoptionId { get; set; }
    }
}
