using Notes.Domain.Primitives;

namespace Notes.Domain.Notes;

public sealed class NoteText : Entity
{
    internal NoteText(Guid id, string text, Guid noteId) 
        : base(id) 
    {
        Text = text;
        NoteId = noteId;
    }

    public string Text { get; private set; }
    public Guid NoteId { get; init; }

    public void UpdateText(string text)
    {
        Text = text;
    }
}
