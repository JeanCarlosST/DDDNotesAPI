namespace Notes.Domain.Notes;

public interface INoteRepository
{
    Task<Note> GetById(Guid id);

}
