﻿using Microsoft.EntityFrameworkCore;
using Notes.Domain.Notes;

namespace Notes.Infrastructure.Repositories;

public class NoteRepository(AppDbContext appDbContext) : INoteRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public void Add(Note note)
    {
        _appDbContext.Notes.Add(note);
    }

    public async Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext
            .Notes
            .Include(x => x.Texts)
            .Include(x => x.Checkboxes)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Remove(Note note)
    {
        _appDbContext.Notes.Remove(note);
    }

    public void Update(Note note)
    {
        _appDbContext.Notes.Update(note);
    }
}
