using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Labels;
using Notes.Infrastructure;
using Notes.Shared;

namespace Notes.Application.Labels.Queries;

public record GetLabelQuery(Guid LabelId) : IRequest<Result<LabelResponse>>;

public sealed class GetLabelQueryHandler(
    AppDbContext appDbContext) : IRequestHandler<GetLabelQuery, Result<LabelResponse>>
{
    private readonly AppDbContext _appDbContext = appDbContext;

    async Task<Result<LabelResponse>> IRequestHandler<GetLabelQuery, Result<LabelResponse>>.Handle(GetLabelQuery request, CancellationToken cancellationToken)
    {
        LabelResponse? label = await _appDbContext
            .Labels
            .Where(x => x.Id == request.LabelId)
            .Select(label => new LabelResponse(label.Id, label.Name))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (label is null)
        {
            return Result.Failure<LabelResponse>(LabelErrors.LabelNotFound);
        }

        return label;
    }
}

