using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface IWarningQueryService
    {
        Task<IEnumerable<Warning>> GetAllAsync();
        Task<Warning> GetByIdAsync(int id);
    }
}
