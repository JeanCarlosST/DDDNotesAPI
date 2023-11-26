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

    public void UpdateTitle(string title)
    {
        Title = title;
    }

    public void UpdateLabelId(Guid? labelId)
    {
        LabelId = labelId;
    }

    public void ClearTexts()
    {
        _texts.Clear();
    }

    public void ClearCheckboxes()
    {
        _checkboxes.Clear();
    }
}
