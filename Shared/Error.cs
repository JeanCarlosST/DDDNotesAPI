namespace Notes.Shared;

public sealed record Error(string code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value provided");

    public static implicit operator Result(Error error) => Result.Failure(error);
}
