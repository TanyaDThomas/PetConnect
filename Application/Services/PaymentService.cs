using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;

namespace PetConnect.Application.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<Payment> CreateAsync(Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeactivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
