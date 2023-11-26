using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Labels.Queries;
using Notes.Domain;
using Notes.Domain.Labels;
using Notes.Domain.Notes;
using Notes.Infrastructure;
using Notes.Shared;

namespace Notes.Application.Notes.Queries;

public record GetNotesQuery : IRequest<Result<List<NoteResponse>>>;

public sealed class GetNotesQueryHandler(
    AppDbContext appDbContext,
    ILabelRepository labelRepository) : IRequestHandler<GetNotesQuery, Result<List<NoteResponse>>>
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly ILabelRepository _labelRepository = labelRepository;

    public async Task<Result<List<NoteResponse>>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        List<Note> notes = await _appDbContext
            .Notes
            .Include(n => n.Texts)
            .Include(n => n.Checkboxes)
            .ToListAsync(cancellationToken);

        List<NoteResponse> noteResponseList = [];

        foreach (Note note in notes)
        {

            List<INoteElement> elements =
            [
                .. note
                    .Texts
                    .Select(t => new NoteTextReponse(t.Id, t.Text, t.Order)),
                .. note
                    .Checkboxes
                    .Select(t => new NoteCheckboxReponse(t.Id, t.Text, t.IsChecked, t.Order)),
            ];

            LabelResponse? labelResponse = null;

            if (note.LabelId is not null)
            {
                Label? label = await _labelRepository.GetByIdAsync(note.LabelId.Value, cancellationToken);

                if (label is not null)
                {
                    labelResponse = new LabelResponse(
                        label.Id,
                        label.Name);
                }
            }

            NoteResponse NoteResponse = new(
                note.Id,
                note.Title,
                labelResponse,
                [.. elements.OrderBy(e => e.Order)]);

            noteResponseList.Add(NoteResponse);
        }

        return noteResponseList;
    }
}
