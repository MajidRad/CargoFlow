using CargoFlow.BuildingBlocks.Domain;

namespace CargoFlow.Identity.Domain.ValueObjects;

public sealed record FullName(
    string FirstName,
    string LastName):IValueObject
{
    public static FullName Create(
        string firstName,
        string lastName)
    {
        return new FullName(
            firstName.Trim(),
            lastName.Trim());
    }
}