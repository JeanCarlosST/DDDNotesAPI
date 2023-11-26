namespace Notes.Domain.Notes;

public interface INoteRepository
{
    Task<Note?> GetByIdAsync(Guid id);

    void Add(Note note);

    void Update(Note note);

    void Remove(Note note);
}
