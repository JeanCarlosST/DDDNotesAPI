﻿using Notes.Domain.Primitives;

namespace Notes.Domain.Notes;

public class NoteCheckbox : Entity
{
    internal NoteCheckbox(Guid id, string text, bool isChecked, int order, Guid noteId) : base(id)
    {
        Text = text;
        IsChecked = isChecked;
        NoteId = noteId;
        Order = order;
    }

    public string Text { get; private set; }
    public bool IsChecked { get; private set; }
    public Guid NoteId { get; init; }
    public int Order { get; private set; }

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
    public void UpdateOrder(int order)
    {
        Order = order;
    }
}
