namespace Notes.Domain.Notes;

public interface INoteRepository
{
    Task<Note?> GetByIdAsync(Guid id);

    void Add(Note note);
}
