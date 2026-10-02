using CargoFlow.BuildingBlocks.Domain;

namespace CargoFlow.Identity.Domain.ValueObjects;

public sealed record PasswordHash : IValueObject
{
    public string Value { get; }
    private PasswordHash(string value)
    {
        Value = value;
    }
    public static PasswordHash Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password hash is required.");

        return new PasswordHash(value);
    }
    public override string ToString()
    => Value;

}