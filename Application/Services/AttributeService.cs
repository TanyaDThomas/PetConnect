using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;

namespace PetConnect.Application.Services
{
    public class AttributeDefinitionService : IAttributeDefinitionService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<AttributeDefinitionService> _logger;
        public AttributeDefinitionService(PetConnectDbContext context, ILogger<AttributeDefinitionService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(AttributeDefinition attributeDefinition)
        {
            try
            {
                _context.AttributeDefinitions.Add(attributeDefinition);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Attribute Definition created with Id {AttributeDefinitionID}", attributeDefinition.Id);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Attribut Definition not created.");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(AttributeDefinition attributeDefinition)
        {
            try
            {
           
                _context.AttributeDefinitions.Update(attributeDefinition);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Attribute Definition updated with Id {AttributeDefinitionId}", attributeDefinition.Id);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Attribute Definition not updated.");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var attribute = await _context.AttributeDefinitions.FirstOrDefaultAsync(ad => ad.Id == id);
                if (attribute == null) return false;

                _context.AttributeDefinitions.Remove(attribute);
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogWarning("Attribute Definition deleted with Id {AttributeId}", attribute.Id);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Attribute Definition not deleted.");
                return false;
            }
        }

    }
}
