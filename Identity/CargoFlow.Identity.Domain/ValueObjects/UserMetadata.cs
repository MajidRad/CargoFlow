using CargoFlow.BuildingBlocks.Domain;

namespace CargoFlow.Identity.Domain.ValueObjects;

public sealed record UserMetadata(string? Language, string? TimeZone, string? Theme) : IValueObject
{
    public static UserMetadata CreateDefault() =>
        new(Language: "en", TimeZone: "UTC", Theme: "light");

    public UserMetadata Update(string? language, string? timeZone, string theme)
        => new UserMetadata(
            Language: language ?? Language,
            TimeZone: timeZone ?? TimeZone,
            Theme: theme ?? theme
            );
}