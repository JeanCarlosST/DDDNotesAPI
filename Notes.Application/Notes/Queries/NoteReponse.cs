﻿namespace Notes.Application.Notes.Queries;

public record NoteReponse(
    Guid NoteId,
    string Title,
    List<INoteElement> Elements);

public interface INoteElement
{
    public int Order { get; init; }
}

public record NoteTextReponse(
    Guid NoteTextId,
    string Text,
    int Order) : INoteElement;

public record NoteCheckboxReponse(
    Guid NoteCheckboxId,
    string Text,
    bool isChecked,
    int Order) : INoteElement;
