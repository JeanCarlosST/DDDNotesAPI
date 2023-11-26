using MediatR;
using Notes.Domain;
using Notes.Domain.Notes;
using System.Text.Json;

namespace Notes.Application.Notes.Commands;

public record CreateNoteCommand(
    string Title,
    Guid labelId,
    IEnumerable<object> elements) : IRequest
{
}

public class CreateNoteCommandHandler(
    INoteRepository noteRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateNoteCommand>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        Note note = Note.Create(
            request.Title,
            request.labelId);

        for(int i = 0; i < request.elements.Count(); i++)
        {
            JsonElement? element = request.elements.ElementAt(i) as JsonElement?;

            if (element == null)
                continue;

            switch (element.Value.ValueKind) {
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

        _noteRepository.Add(note);

        return _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
