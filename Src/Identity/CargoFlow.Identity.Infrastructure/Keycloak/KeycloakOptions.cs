namespace CargoFlow.Identity.Infrastructure.Keycloak;

public sealed class KeycloakOptions
{
    public const string SectionName = "Keycloak";
    public string BaseUrl { get; init; } = default!;
    public string Realm { get; init; } = default!;
    public string ClientId { get; init; } = default!;
    public string ClientSecret {  get; init; } = default!;
    public string AdminUserName {  get; init; } = default!;
    public string AdminPassword {  get; init; } = default!;
}
