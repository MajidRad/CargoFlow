using CargoFlow.Identity.Infrastructure.Keycloak.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Clients;

public sealed class KeycloakAuthClient : IKeycloakAuthClient
{
    private readonly KeycloakOptions _options;
    private readonly HttpClient _httpClient;

    public KeycloakAuthClient(HttpClient httpClient, KeycloakOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }
    public async Task<TokenResponse?> LoginAsync(string username, string password, CancellationToken cancellation = default)
    {
        var form = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["grant_type"] = "password",
            ["username"] = username,
            ["password"] = password
        };
        var response = await _httpClient.PostAsync(
            GetTokenEndpoint(),
            new FormUrlEncodedContent(form),
            cancellation
            );
        response.EnsureSuccessStatusCode();
        return await response
            .Content
            .ReadFromJsonAsync<TokenResponse>(cancellation);
    }

    public async Task<TokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var form = new Dictionary<string, string>
        {
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["grant_type"] = "refresh_token",
            ["refresh_token"] = refreshToken,
        };
        var response = await _httpClient.PostAsync(
            GetTokenEndpoint(),
            new FormUrlEncodedContent(form),
            cancellationToken
            );
        response.EnsureSuccessStatusCode();
        return await response.Content
            .ReadFromJsonAsync<TokenResponse>(cancellationToken);
    }
    private string GetTokenEndpoint()
    {
        return $"/realms/{_options.Realm}/protocol/openid-connect/token";
    }
}
