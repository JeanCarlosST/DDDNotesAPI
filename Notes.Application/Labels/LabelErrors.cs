using Shared;

namespace Notes.Application.Labels;

public static class LabelErrors
{
    public static Error LabelNotFound => new("Label.NotFound", "Label not found");
}
