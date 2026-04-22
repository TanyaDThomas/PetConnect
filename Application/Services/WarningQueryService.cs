using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;

namespace PetConnect.Application.Services
{
    public class WarningQueryService : IWarningQueryService
    {
        private readonly PetConnectDbContext _context;
        public WarningQueryService(PetConnectDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Warning>> GetAllAsync()
        {
            return await _context.Warnings
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<Warning> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
