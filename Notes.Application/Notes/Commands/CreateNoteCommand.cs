using MediatR;
using Notes.Domain;
using Notes.Domain.Notes;
using Shared;
using System.Text.Json;

namespace Notes.Application.Notes.Commands;

public record CreateNoteCommand(
    string Title,
    Guid LabelId,
    IEnumerable<object> Elements) : IRequest<Result>;

public class CreateNoteCommandHandler(
    INoteRepository noteRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateNoteCommand, Result>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        Note note = Note.Create(
            request.Title,
            request.LabelId);

        try
        {
            for (int i = 0; i < request.Elements.Count(); i++)
            {
                JsonElement? element = request.Elements.ElementAt(i) as JsonElement?;

                if (element is null)
                    continue;

                switch (element.Value.ValueKind)
                {
                    case JsonValueKind.String:
                        note.AddText(element.Value.GetString()!, i);
                        break;
                    case JsonValueKind.Object:
                        note.AddCheckbox(
                            element.Value.GetProperty("text").GetString()!,
                            element.Value.GetProperty("isChecked").GetBoolean()!,
                            i);
                        break;
                }
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
