using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Domain.Labels;
using Notes.Infrastructure;
using Notes.Shared;

namespace Notes.Application.Labels.Commands;

public record DeleteLabelCommand(Guid LabelId) : IRequest<Result>;

public class DeleteLabelCommandHandler(
    ILabelRepository labelRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteLabelCommand, Result>
{
    private readonly ILabelRepository _labelRepository = labelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteLabelCommand request, CancellationToken cancellationToken)
    {
        Label? label = await _labelRepository.GetByIdAsync(request.LabelId, cancellationToken);

        if (label is null)
        {
            return LabelErrors.LabelNotFound;
        }

        _labelRepository.Remove(label);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
