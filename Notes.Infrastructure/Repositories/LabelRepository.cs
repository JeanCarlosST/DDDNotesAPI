using Microsoft.EntityFrameworkCore;
using Notes.Domain.Labels;

namespace Notes.Infrastructure.Repositories;

public class LabelRepository(AppDbContext appDbContext) : ILabelRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public void Add(Label label)
    {
        _appDbContext.Add(label);
    }

    public void Remove(Label label)
    {
        _appDbContext.Remove(label);
    }

    public void Update(Label label)
    {
        _appDbContext.Update(label);
    }
}
