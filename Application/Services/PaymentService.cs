using Microsoft.AspNetCore.Http.HttpResults;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PetConnect.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(PetConnectDbContext context, ILogger<PaymentService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(PaymentViewModel viewModel)
        {
            try
            {
                var adoption = await _context.Adoptions.FirstOrDefaultAsync(a => a.Id == viewModel.AdoptionId);
                if (adoption == null) return false;

                var payment = new Payment()
                {
                    Amount = adoption.AdoptionFee,
                    Type = viewModel.Type,
                    AdopterId = viewModel.AdopterId,
                    AdoptionId = viewModel.AdoptionId,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    LastFourDigits= viewModel.LastFourDigits,
                    PaypalEmail = viewModel.PaypalEmail,
                    BankName = viewModel.BankName,
                    CheckNumber = viewModel.CheckNumber,
                };

                switch (viewModel.Type)
                {
                    case PaymentType.CreditCard:
                        if (string.IsNullOrWhiteSpace(viewModel.FirstName) ||
                            string.IsNullOrWhiteSpace(viewModel.LastName))
                        {
                            throw new InvalidOperationException("Card payments require first and last name.");

                        }
                        payment.FirstName = viewModel.FirstName;
                        payment.LastName = viewModel.LastName;
                        break;

                    case PaymentType.PayPal:
                        if (string.IsNullOrWhiteSpace(viewModel.PaypalEmail))
                        {
                            throw new InvalidOperationException("PayPal email is required.");

                        }
                        payment.PaypalEmail = viewModel.PaypalEmail;
                        break;

                    case PaymentType.Check:
                        payment.BankName = viewModel.BankName;
                        payment.CheckNumber = viewModel.CheckNumber;
                        break;

                    case PaymentType.Cash:
                        payment.FirstName = viewModel.FirstName;
                        payment.LastName = viewModel.LastName;
                        break;
                }

              

                payment.CreatedOn= DateTime.UtcNow;
                payment.CreatedBy = "System";

                _context.Payments.Add(payment);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Payment created with Id {PaymentId}",
                    payment.Id);

                return rowsAffected > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment was not created.");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(PaymentViewModel viewModel)
        {
            try
            {
                var payment = await _context.Payments.FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                if (payment == null) return false;

                payment.Amount = viewModel.Amount;
                    payment.Type = viewModel.Type;
                payment.AdopterId = viewModel.AdopterId;
                payment.AdoptionId = viewModel.AdoptionId;
                payment.FirstName = viewModel.FirstName;
                payment.LastName = viewModel.LastName;
                payment.LastFourDigits = viewModel.LastFourDigits;
                payment.PaypalEmail = viewModel.PaypalEmail;
                payment.BankName = viewModel.BankName;
                    payment.CheckNumber = viewModel.CheckNumber;

                _context.Payments.Update(payment);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogWarning("POST UPDATE Id = {Id}", viewModel.Id);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment did not update.");
                return false;
            }
        }

        public async Task<bool> DeactivateAsync(int id)
        {
           try
            {
                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
                if (payment == null) return false;

                payment.UpdatedOn = DateTime.UtcNow;
                payment.UpdatedBy = "System";
                
                payment.IsActive = false;
                var rowsAffected = await _context.SaveChangesAsync();
                
                _logger.LogWarning("Payment deactivated by ID {PaymentId}", id);

                return rowsAffected > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment was not deactivated.");
                return false;
            }
        }

   
    }
}
