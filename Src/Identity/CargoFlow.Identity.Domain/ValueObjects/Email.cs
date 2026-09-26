using CargoFlow.BuildingBlocks.Domain;
using CargoFlow.Identity.Domain.Exceptions;

namespace CargoFlow.Identity.Domain.ValueObjects;

public sealed record Email(string Value) : IValueObject
{
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
            throw new InvalidEmailException(value);
        return new Email(value.ToLowerInvariant());
    }
    public override string ToString()=>Value;

}