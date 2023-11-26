using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Labels;
using Notes.Infrastructure;

namespace Notes.Application.Labels.Queries;

public record GetLabelQuery(Guid LabelId) : IRequest<LabelResponse>;

public sealed class GetLabelQueryHandler(
    AppDbContext appDbContext) : IRequestHandler<GetLabelQuery, LabelResponse?>
{
    private readonly AppDbContext _appDbContext = appDbContext;

    async Task<LabelResponse?> IRequestHandler<GetLabelQuery, LabelResponse?>.Handle(GetLabelQuery request, CancellationToken cancellationToken)
    {
        LabelResponse? label = await _appDbContext
            .Labels
            .Where(x => x.Id == request.LabelId)
            .Select(label => new LabelResponse(label.Id, label.Name))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return label;
    }
}

