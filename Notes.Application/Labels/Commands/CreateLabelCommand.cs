using MediatR;
using Notes.Domain;
using Notes.Domain.Labels;

namespace Notes.Application.Labels.Commands;

public record CreateLabelCommand(string Name) : IRequest;

public class CreateLabelCommandHandler(
    ILabelRepository labelRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateLabelCommand>
{
    private readonly ILabelRepository _labelRepository = labelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    async Task IRequestHandler<CreateLabelCommand>.Handle(CreateLabelCommand request, CancellationToken cancellationToken)
    {
        Label label = Label.Create(request.Name);

        _labelRepository.Add(label);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
