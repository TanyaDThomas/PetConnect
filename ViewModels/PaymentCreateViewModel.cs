using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetConnect.ViewModels
{
    public class PaymentCreateViewModel
    {
      
            public decimal Amount { get; set; }

            public PaymentType Type { get; set; }

            public int? AdopterId { get; set; }
            public int? AdoptionId { get; set; }

            // Card
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? LastFourDigits { get; set; }

            // PayPal
            public string? PaypalEmail { get; set; }

            // Check
            public string? BankName { get; set; }
            public string? CheckNumber { get; set; }
        

    }
}
