using Notes.Domain.Primitives;

namespace Notes.Domain.Notes;

public sealed class NoteText : Entity
{
    internal NoteText(Guid id, string text, int order, Guid noteId) 
        : base(id) 
    {
        Text = text;
        NoteId = noteId;
        Order = order;
    }

    public string Text { get; private set; }
    public Guid NoteId { get; init; }
    public int Order { get; private set; }

    public void UpdateText(string text)
    {
        Text = text;
    }

    public void UpdateOrder(int order)
    {
        Order = order;
    }
}
