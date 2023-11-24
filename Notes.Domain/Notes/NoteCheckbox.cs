using Notes.Domain.Primitives;

namespace Notes.Domain.Notes;

public class NoteCheckbox : Entity
{
    internal NoteCheckbox(Guid id, string text, bool isChecked, Guid noteId) : base(id)
    {
        Text = text;
        IsChecked = isChecked;
        NoteId = noteId;
    }

    public string Text { get; private set; }
    public bool IsChecked { get; private set; }
    public Guid NoteId { get; init; }

    public void UpdateText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        Text = text;
    }

    public void UpdateIsChecked(bool isChecked)
    {
        IsChecked = isChecked; 
    }
}
