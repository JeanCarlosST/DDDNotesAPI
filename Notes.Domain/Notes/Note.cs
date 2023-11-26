using Notes.Domain.Labels;
using Notes.Domain.Primitives;

namespace Notes.Domain.Notes;

public sealed class Note : AggregateRoot
{
    private Note(
        Guid id,
        string title,
        Guid? labelId) : base(id)
    {
        Title = title;
        LabelId = labelId;
    }

    public string Title { get; private set; }
    public Guid? LabelId { get; private set; }

    private readonly HashSet<NoteText> _texts = [];
    private readonly HashSet<NoteCheckbox> _checkboxes = [];

    public IReadOnlyCollection<NoteText> Texts => _texts.ToList();
    public IReadOnlyCollection<NoteCheckbox> Checkboxes => _checkboxes.ToList();

    public static Note Create(
        string title, 
        Guid? labelId)
    {
        return new Note(
            Guid.NewGuid(), 
            title,
            labelId);
    }

    public void AddText(string text, int order)
    {
        _texts.Add(new NoteText(Guid.NewGuid(), text, order, Id));
    }

    public void AddCheckbox(string text, bool isChecked, int order)
    {
        _checkboxes.Add(new NoteCheckbox(Guid.NewGuid(), text, isChecked, order, Id));
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

    public void SetLabel(Guid? labelId)
    {
        LabelId = labelId;
    }
}
