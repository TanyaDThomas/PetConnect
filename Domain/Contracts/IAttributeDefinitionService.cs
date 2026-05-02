using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface IAttributeDefinitionService
    {
        Task<bool> CreateAsync(AttributeDefinition attributeDefinition);
        Task<bool> UpdateAsync(AttributeDefinition attributeDefinition);
        Task<bool> DeleteAsync(int id);
    }
}
