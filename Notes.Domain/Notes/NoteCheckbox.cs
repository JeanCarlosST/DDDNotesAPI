using Notes.Domain.Primitives;

namespace Notes.Domain.Notes;

internal class NoteCheckbox : Entity
{
    internal NoteCheckbox(Guid id, string text, bool isChecked) : base(id)
    {
        Text = text;
        IsChecked = isChecked;
    }

    public string Text { get; private set; }
    public bool IsChecked { get; private set; }

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
