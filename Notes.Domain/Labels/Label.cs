using Notes.Domain.Notes;
using Notes.Domain.Primitives;

namespace Notes.Domain.Labels;

public sealed class Label : Entity
{
    private Label(Guid id, string name) 
        : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public static Label Create(string name)
    {
        return new Label(Guid.NewGuid(), name);
    }

    private readonly HashSet<Note> _notes = [];
}
