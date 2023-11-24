using Notes.Domain.Labels;
using Notes.Domain.Primitives;

namespace Notes.Domain.Notes;

public sealed class Note : AggregateRoot
{
    private Note(
        Guid id,
        string title,
        Label label) : base(id)
    {
        Title = title;
        Label = label;
    }

    public string Title { get; private set; }
    public Label Label { get; private set; }

    private readonly HashSet<NoteText> _texts = [];
    private readonly HashSet<NoteCheckbox> _checkboxes = [];

    public static Note Create(
        string title, 
        Label label)
    {
        return new Note(
            Guid.NewGuid(), 
            title, 
            label);
    }

    public void AddText(string text)
    {
        _texts.Add(new NoteText(Guid.NewGuid(), text));
    }

    public void AddCheckbox(string text, bool isChecked)
    {
        _checkboxes.Add(new NoteCheckbox(Guid.NewGuid(), text, isChecked));
    }

    public void UpdateText(Guid id, string text)
    {
        var noteText = _texts.FirstOrDefault(t => t.Id == id);

        if(noteText == null)
        {
            return;
        }

        if(string.IsNullOrEmpty(text))
        {
            RemoveText(id);
        }
        else
        {
            noteText.UpdateText(text);
        }
    }

    public void UpdateCheckbox(Guid id, bool isChecked)
    {
        var checkbox = _checkboxes.FirstOrDefault(c => c.Id == id);

        if(checkbox == null)
        {
            return;
        }

        checkbox.UpdateIsChecked(isChecked);
    }

    public void UpdateCheckboxText(Guid id, string text)
    {
        var checkbox = _checkboxes.FirstOrDefault(c => c.Id == id);

        if(checkbox == null)
        {
            return;
        }

        checkbox.UpdateText(text);
    }

    public void RemoveText(Guid id)
    {
        var noteText = _texts.FirstOrDefault(t => t.Id == id);

        if(noteText == null)
        {
            return;
        }

        _texts.Remove(noteText);
    }

    public void RemoveCheckbox(Guid id)
    {
        var checkbox = _checkboxes.FirstOrDefault(c => c.Id == id);

        if(checkbox == null)
        {
            return;
        }

        _checkboxes.Remove(checkbox);
    }
}
