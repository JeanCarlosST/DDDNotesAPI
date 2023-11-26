using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Labels;
using System.Threading;

namespace Notes.Infrastructure.Repositories;

public class LabelRepository(AppDbContext appDbContext) : ILabelRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public void Add(Label label)
    {
        _appDbContext.Labels.Add(label);
    }

    public async Task<Label?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Label? label = await _appDbContext
            .Labels
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return label;
    }

    public void Remove(Label label)
    {
        _appDbContext.Labels.Remove(label);
    }

    public void Update(Label label)
    {
        _appDbContext.Labels.Update(label);
    }
}
