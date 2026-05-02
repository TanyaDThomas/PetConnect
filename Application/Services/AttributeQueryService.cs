using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;

namespace PetConnect.Application.Services
{
    public class AttributeDefinitionQueryService : IAttributeDefinitionQueryService
    {
        private readonly PetConnectDbContext _context;
        public AttributeDefinitionQueryService(PetConnectDbContext context)
        {
            _context = context;
        }

    

        public async Task<IEnumerable<AttributeDefinition>> GetAllAsync()
        {
            return await _context.AttributeDefinitions
                       .AsNoTracking()
                       .ToListAsync();
        }

        public async Task<AttributeDefinition?> GetByIdAsync(int id)
        {
            return await _context.AttributeDefinitions.FirstOrDefaultAsync(ad => ad.Id == id);
        }
    }
}
