using CargoFlow.BuildingBlocks.Domain;


namespace CargoFlow.Identity.Domain.ValueObjects;

public sealed record Email : IValueObject
{
    public string Value { get; }
    private Email(string value)
    {
        Value = value;
    }
    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required");
        }

        if (!email.Contains('@'))
            throw new ArgumentException("Invalid email");
        return new Email(email.Trim().ToLowerInvariant());
    }

    public override string ToString()
        => Value;
}
