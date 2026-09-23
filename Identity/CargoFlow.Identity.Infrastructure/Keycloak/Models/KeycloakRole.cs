using System.Text.Json.Serialization;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Models;

public sealed class KeycloakRole
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; }= default!;

}
