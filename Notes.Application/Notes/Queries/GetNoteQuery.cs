using MediatR;
using Notes.Domain.Notes;

namespace Notes.Application.Notes.Queries;

public record GetNoteQuery(Guid NoteId) : IRequest<NoteReponse?>;

public sealed class GetNoteQueryHandler(
    INoteRepository noteRepository) : IRequestHandler<GetNoteQuery, NoteReponse?>
{
    private readonly INoteRepository _noteRepository = noteRepository;

    async Task<NoteReponse?> IRequestHandler<GetNoteQuery, NoteReponse?>.Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        Note? note = await _noteRepository.GetByIdAsync(request.NoteId);

        if (note == null)
        {
            return null;
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

        NoteReponse response = new(
            note.Id,
            note.Title,
            [.. elements.OrderBy(e => e.Order)]);

        return await Task.FromResult(response);
    }
}

