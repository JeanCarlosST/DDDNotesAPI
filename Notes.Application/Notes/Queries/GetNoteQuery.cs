using MediatR;
using Notes.Application.Labels.Queries;
using Notes.Domain.Labels;
using Notes.Domain.Notes;
using Notes.Shared;

namespace Notes.Application.Notes.Queries;

public record GetNoteQuery(Guid NoteId) : IRequest<Result<NoteResponse>>;

public sealed class GetNoteQueryHandler(
    INoteRepository noteRepository,
    ILabelRepository labelRepository) : IRequestHandler<GetNoteQuery, Result<NoteResponse>>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly ILabelRepository _labelRepository = labelRepository;

    async Task<Result<NoteResponse>> IRequestHandler<GetNoteQuery, Result<NoteResponse>>.Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        Note? note = await _noteRepository.GetByIdAsync(request.NoteId, cancellationToken);

        if (note is null)
        {
            return Result.Failure<NoteResponse>(NoteErrors.NoteNotFound);
        }

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
        
        if(note.LabelId is not null)
        {
            Label? label = await _labelRepository.GetByIdAsync(note.LabelId.Value, cancellationToken);

            if(label is not null)
            {
                labelResponse = new LabelResponse(
                    label.Id,
                    label.Name);
            }
        }

        NoteResponse response = new(
            note.Id,
            note.Title,
            labelResponse,
            [.. elements.OrderBy(e => e.Order)]);

        return response;
    }
}