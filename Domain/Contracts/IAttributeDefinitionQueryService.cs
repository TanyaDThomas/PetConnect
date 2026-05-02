using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface IAttributeDefinitionQueryService
    {
        Task<IEnumerable<AttributeDefinition>> GetAllAsync();
        Task<AttributeDefinition?> GetByIdAsync(int id);
    }
}
