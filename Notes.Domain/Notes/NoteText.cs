using Notes.Domain.Primitives;

namespace Notes.Domain.Notes;

public sealed class NoteText : Entity
{
    internal NoteText(Guid id, string text) 
        : base(id) 
    {
        Text = text;
    }

    public string Text { get; private set; }

    public void UpdateText(string text)
    {
        Text = text;
    }
}
