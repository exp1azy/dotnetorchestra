using DotNetOrchestra.Server.Data;
using DynamicDI;
using Microsoft.EntityFrameworkCore;

namespace DotNetOrchestra.Server.Repositories
{
    [RegisterService]
    public class NoteRepository
    {
        private readonly DataContext _dbContext;

        public NoteRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Note[]> GetNotesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Notes.AsNoTracking().ToArrayAsync(cancellationToken);
        }

        public async Task<Note?> GetNoteAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Notes.AnyAsync(x => x.Name == name, cancellationToken);
        }

        public async Task<Note> CreateNoteAsync(Note note, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.Notes.AddAsync(note, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<Note> UpdateNoteAsync(Note note, CancellationToken cancellationToken = default)
        {
            var result = _dbContext.Notes.Update(note);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<bool> RemoveNoteAsync(int id, CancellationToken cancellationToken = default)
        {
            var noteToRemove = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (noteToRemove == null) return false;

            _dbContext.Notes.Remove(noteToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
