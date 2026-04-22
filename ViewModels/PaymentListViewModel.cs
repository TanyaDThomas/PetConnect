using PetConnect.Domain.Entities;

namespace PetConnect.ViewModels
{
    public class PaymentListViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set;  }
        public string PaymentMethod { get; set; } = "";
        public string Status { get; set; } = "";
       
    }
}
