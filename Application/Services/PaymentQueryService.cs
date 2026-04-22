using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;

namespace PetConnect.Application.Services
{
    public class PaymentQueryService : IPaymentQueryService
    {
        private readonly PetConnectDbContext _context;
        public PaymentQueryService(PetConnectDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
