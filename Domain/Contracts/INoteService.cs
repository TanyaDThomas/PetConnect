using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface INoteService
    {
        Task<Note> CreateAsync(Note note, string userName);
        Task<bool> UpdateAsync(Note note, string userName);
        Task<bool> DeactivateAsync(int id, string userName);

    }
}
