using System.Text.Json.Serialization;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Models;

public sealed class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = default!;

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; } = default!;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init;  }

    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = default!;

}
