using PetConnect.Domain.Entities;

namespace PetConnect.Domain.Contracts
{
    public interface INoteService
    {
        Task<Note> CreateAsync(Note note);
        Task<bool> UpdateAsync(Note note);
        Task<bool> DeactivateAsync(int id);

    }
}
