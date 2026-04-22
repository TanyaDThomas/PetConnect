using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface IPaymentQueryService
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment> GetByIdAsync(int id);
    }
}
