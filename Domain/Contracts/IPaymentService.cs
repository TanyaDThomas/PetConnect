using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IPaymentService
    {
        Task<bool> CreateAsync(PaymentViewModel viewModel, string userName);
        Task<bool> UpdateAsync(PaymentViewModel viewModel, string userName);
        Task<bool> DeactivateAsync(int id, string userName);
    }
}
