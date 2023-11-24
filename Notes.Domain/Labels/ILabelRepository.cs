namespace Notes.Domain.Labels;

public interface ILabelRepository
{
    Task<Label?> GetByIdAsync(Guid id);

    void Add(Label label);

    void Update(Label label);

    void Remove(Label label);
}
