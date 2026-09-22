using CargoFlow.BuildingBlocks.Domain;

namespace CargoFlow.Identity.Domain.ValueObjects;

public record struct UserId(Guid Value) : IValueObject
{
    public static UserId New() => new(Guid.NewGuid());
}
