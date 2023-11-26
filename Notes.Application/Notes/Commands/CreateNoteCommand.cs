using MediatR;
using Notes.Application.Labels;
using Notes.Domain;
using Notes.Domain.Labels;
using Notes.Domain.Notes;
using Shared;
using System.Text.Json;

namespace Notes.Application.Notes.Commands;

public record CreateNoteCommand(
    string Title,
    Guid? LabelId,
    IEnumerable<object> Elements) : IRequest<Result>;

public class CreateNoteCommandHandler(
    INoteRepository noteRepository,
    ILabelRepository labelRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateNoteCommand, Result>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly ILabelRepository _labelRepository = labelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        if(request.LabelId is not null)
        {
            Label? label = await _labelRepository.GetByIdAsync(request.LabelId.Value, cancellationToken);

            if(label is null)
            {
                return Result.Failure(LabelErrors.LabelNotFound);
            }
        }

        Note note = Note.Create(
            request.Title,
            request.LabelId);

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
            return NoteErrors.ParsingElements;
        }

        _noteRepository.Add(note);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
