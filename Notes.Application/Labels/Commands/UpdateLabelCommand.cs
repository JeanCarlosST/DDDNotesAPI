using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Labels.Queries;
using Notes.Domain;
using Notes.Domain.Labels;
using Notes.Infrastructure;
using Shared;

namespace Notes.Application.Labels.Commands;

public record UpdateLabelCommand(Guid LabelId, string Name) : IRequest<Result<LabelResponse>>;

public sealed class UpdateLabelCommandHandler(
    ILabelRepository labelRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateLabelCommand, Result<LabelResponse>>
{
    private readonly ILabelRepository _labelRepository = labelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<LabelResponse>> Handle(UpdateLabelCommand request, CancellationToken cancellationToken)
    {
        Label? label = await _labelRepository.GetByIdAsync(request.LabelId, cancellationToken);

        if(label is null)
        {
            return Result.Failure<LabelResponse>(LabelErrors.LabelNotFound);
        }

        label.UpdateName(request.Name);

        _labelRepository.Update(label);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new LabelResponse(label.Id, label.Name));
    }
}
