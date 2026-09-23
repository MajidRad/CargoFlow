using CargoFlow.Identity.Infrastructure.Keycloak.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CargoFlow.Identity.Infrastructure.Keycloak.TokenProvider;

public interface IKeycloakTokenProvider
{
    public Task<string> GetAdminAccessTokenAsync(CancellationToken cancellationToken=default);
}
public sealed class KeycloakTokenProvider:IKeycloakTokenProvider
{
    private readonly KeycloakOptions _options;
    private readonly HttpClient _httpClient;

    private readonly SemaphoreSlim _lock = new(1, 1);
    private string? _accessToken;
    private DateTime _expireAtUtc;

    public KeycloakTokenProvider(KeycloakOptions options,HttpClient httpClient)
    {
        _options = options;
        _httpClient = httpClient;
    }
    public async Task<string> GetAdminAccessTokenAsync(CancellationToken cancellationToken=default)
    {
        if (IsTokenValid())
        {
            return _accessToken!;
        }
        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (IsTokenValid())
            {
                return _accessToken!;
            }
            var token = await RequestTokenAsync(cancellationToken);
            _accessToken = token.AccessToken;
            _expireAtUtc = DateTime.UtcNow.AddSeconds(token.ExpiresIn - 60);
            return _accessToken;
        }
        finally{
            _lock.Release();            
        }
    }
    private async Task<TokenResponse> RequestTokenAsync(CancellationToken cancellationToken)
    {
        var form = new Dictionary<string, string>
        {
            ["client_id"] = "admin-cli",
            ["grant_type"] = "password",
            ["username"] = _options.AdminUserName,
            ["password"] = _options.AdminPassword,
        };
        var response = await _httpClient.PostAsync(
            "/realms/master/protocol/openid-connect/token",
            new FormUrlEncodedContent(form),
            cancellationToken
            );

        response.EnsureSuccessStatusCode();
       return await response.Content
            .ReadFromJsonAsync<TokenResponse>(cancellationToken)
            ?? throw new InvalidOperationException("Failed to retrieve Keycloak admin token.");
    } 
    private bool IsTokenValid()
    {
      return !string.IsNullOrWhiteSpace(_accessToken) 
            && _expireAtUtc > DateTime.UtcNow;  

    }
}