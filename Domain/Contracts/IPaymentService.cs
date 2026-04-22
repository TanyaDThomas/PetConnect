using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface IPaymentService
    {
        Task<Payment> CreateAsync(Payment payment);
        Task<bool> UpdateAsync(Payment payment);
        Task<bool> DeactivateAsync(int id);
    }
}
