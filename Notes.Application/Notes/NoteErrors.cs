using Notes.Shared;

namespace Notes.Application.Notes;

public static class NoteErrors
{
    public static Error NoteNotFound => new("Note.NotFound", "Note not found");
    public static Error ParsingElements => new("Note.ParsingElement", "Error parsing note elements");
}

