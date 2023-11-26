using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Infrastructure;

namespace Notes.Application.Labels.Queries;

public record GetLabelsQuery : IRequest<List<LabelResponse>>;

public sealed class GetLabelsQueryHandler(
    AppDbContext appDbContext) : IRequestHandler<GetLabelsQuery, List<LabelResponse>>
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<List<LabelResponse>> Handle(GetLabelsQuery request, CancellationToken cancellationToken)
    {
        return await _appDbContext
            .Labels
            .Select(label => new LabelResponse(label.Id, label.Name))
            .ToListAsync(cancellationToken);
    }
}
