using MediatR;
using Notes.Domain.Notes;
using Shared;

namespace Notes.Application.Notes.Queries;

public record GetNoteQuery(Guid NoteId) : IRequest<Result<NoteResponse>>;

public sealed class GetNoteQueryHandler(
    INoteRepository noteRepository) : IRequestHandler<GetNoteQuery, Result<NoteResponse>>
{
    private readonly INoteRepository _noteRepository = noteRepository;

    async Task<Result<NoteResponse>> IRequestHandler<GetNoteQuery, Result<NoteResponse>>.Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        Note? note = await _noteRepository.GetByIdAsync(request.NoteId);

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

        NoteResponse response = new(
            note.Id,
            note.Title,
            [.. elements.OrderBy(e => e.Order)]);

        return response;
    }
}