using MediatR;
using Notes.Domain;
using Notes.Domain.Notes;
using Shared;

namespace Notes.Application.Notes.Commands;

public record DeleteNoteCommand(Guid NoteId) : IRequest<Result>;

public sealed class DeleteNoteCommandHandler(
    INoteRepository noteRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteNoteCommand, Result>
{
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        Note? note = await _noteRepository.GetByIdAsync(request.NoteId, cancellationToken);

        if (note is null) 
        {
            return Result.Failure(NoteErrors.NoteNotFound);
        }

        _noteRepository.Remove(note);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
