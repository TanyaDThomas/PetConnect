using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;

namespace PetConnect.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly PetConnectDbContext _context;
        public NoteService(PetConnectDbContext context)
        {
            _context = context;
        }

        //WRITE
        public async Task<Note> CreateAsync(Note note, string userId)
        {
            note.CreatedOn = DateTime.Now;
            note.CreatedBy = userId;
            note.IsActive = true;
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<bool> DeactivateAsync(int id, string userId)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return false;
            note.UpdatedOn = DateTime.Now;
            note.UpdatedBy = userId;
            note.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Note note, string userId)
        {
            try
            {
                note.UpdatedOn = DateTime.Now;
                note.UpdatedBy = userId;
                _context.Entry(note).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

        }

    }
}
