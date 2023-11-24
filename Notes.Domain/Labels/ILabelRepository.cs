namespace Notes.Domain.Labels;

public interface ILabelRepository
{
    Task<Label> GetById(Guid id);

    Task Add(string name);

    Task Update(Guid id, string name);

    Task Remove(Guid id);
}
