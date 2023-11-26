namespace Notes.Domain.Labels;

public interface ILabelRepository
{
    void Add(Label label);

    void Update(Label label);

    void Remove(Label label);
}
