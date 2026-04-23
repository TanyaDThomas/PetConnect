using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IPaymentService
    {
        Task<bool> CreateAsync(PaymentViewModel viewModel);
        Task<bool> UpdateAsync(PaymentViewModel viewModel);
        Task<bool> DeactivateAsync(int id);
    }
}
