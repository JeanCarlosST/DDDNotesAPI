using Notes.Domain;

namespace Notes.Infrastructure;

public sealed class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
