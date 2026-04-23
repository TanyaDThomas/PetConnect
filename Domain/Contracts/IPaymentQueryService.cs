using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Domain.Contracts
{
    public interface IPaymentQueryService
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);

        Task<List<PaymentListViewModel>> GetPaymentListAsync();
        Task<PaymentDetailsViewModel?> GetDetailsAsync(int id);
        Task<PaymentViewModel?> GetPaymentUpdateAsync(int id);

        Task<PaymentViewModel> BuildCreateModelAsync(int? adoptionId);

    }
}
