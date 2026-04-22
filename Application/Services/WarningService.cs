using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;

namespace PetConnect.Application.Services
{
    public class WarningService : IWarningService
    {
        public Task<Warning> CreateAsync(Warning warning)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeactivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Warning warning)
        {
            throw new NotImplementedException();
        }
    }
}
