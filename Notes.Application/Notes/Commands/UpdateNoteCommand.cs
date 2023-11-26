using MediatR;
using Notes.Application.Labels;
using Notes.Application.Labels.Queries;
using Notes.Application.Notes.Queries;
using Notes.Domain;
using Notes.Domain.Labels;
using Notes.Domain.Notes;
using Shared;
using System.Text.Json;

namespace Notes.Application.Notes.Commands;

public record UpdateNoteRequest(
    string Title,
    Guid? LabelId,
    IEnumerable<object> Elements);

public record UpdateNoteCommand(
    Guid NoteId,
    string Title,
    Guid? LabelId,
    IEnumerable<object> Elements) : IRequest<Result<NoteResponse>>;

public sealed class UpdateNoteCommandHandler(
    INoteRepository noteRepository,
    ILabelRepository labelRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateNoteCommand, Result<NoteResponse>>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly ILabelRepository _labelRepository = labelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoteResponse>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        Note? note = await _noteRepository.GetByIdAsync(request.NoteId, cancellationToken);

        if(note is null)
        {
            return Result.Failure<NoteResponse>(NoteErrors.NoteNotFound);
        }

        note.UpdateTitle(request.Title);

        LabelResponse? labelResponse = null;

        if(request.LabelId is not null)
        {
            Label? label = await _labelRepository.GetByIdAsync(request.LabelId.Value, cancellationToken);

            if (label is null)
            {
                return Result.Failure<NoteResponse>(LabelErrors.LabelNotFound);
            }

            labelResponse = new LabelResponse(label.Id, label.Name);
        }

        note.UpdateLabelId(request.LabelId);

        note.ClearTexts();
        note.ClearCheckboxes();

        try
        {
            int order = 0;
            for (int i = 0; i < request.Elements.Count(); i++)
            {
                JsonElement? element = request.Elements.ElementAt(i) as JsonElement?;

                if (element is null)
                    continue;

                switch (element.Value.ValueKind)
                {
                    case JsonValueKind.String:
                        note.AddText(element.Value.GetString()!, order);
                        break;
                    case JsonValueKind.Object:
                        note.AddCheckbox(
                            element.Value.GetProperty("text").GetString()!,
                            element.Value.GetProperty("isChecked").GetBoolean()!,
                            order);
                        break;
                }

                order++;
            }
        }
        catch (Exception)
        {
            return Result.Failure<NoteResponse>(NoteErrors.ParsingElements);
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

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        NoteResponse response = new(
            note.Id,
            note.Title,
            labelResponse,
            elements
            );

        return Result.Success(response);
    }
}
