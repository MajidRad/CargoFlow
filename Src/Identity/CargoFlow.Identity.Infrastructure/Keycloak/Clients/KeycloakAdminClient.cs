using CargoFlow.Identity.Application.DTOs;
using CargoFlow.Identity.Infrastructure.Keycloak.Models;
using CargoFlow.Identity.Infrastructure.Keycloak.TokenProvider;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text.Json;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Clients;

public sealed class KeycloakAdminClient : IkeycloakAdminClient
{
    private readonly KeycloakOptions _options;
    private readonly HttpClient _httpClient;
    private readonly IKeycloakTokenProvider _keycloakTokenProvider;

    public KeycloakAdminClient(IOptions<KeycloakOptions> options, HttpClient httpClient, IKeycloakTokenProvider keycloakTokenProvider)
    {
        _options = options.Value;
        _httpClient = httpClient;
        _keycloakTokenProvider = keycloakTokenProvider;
    }

    public async Task<string> CreateUserAsync(CreateKeycloakUserRequest request, CancellationToken cancellation = default)
    {
        var accessToken = await _keycloakTokenProvider.GetAdminAccessTokenAsync(cancellation);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
        var payload = new KeycloakUserRepresentation
        {
            Username = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Enabled = request.Enabled,
            EmailVerified = request.EmailVerified,
            Credentials =
         [
             new KeycloakCredential
                {
                    Value = request.Password,
                    Temporary = false
                }
         ]
        };
        var response = await _httpClient.PostAsJsonAsync(
            $"/admin/realms/{_options.Realm}/users",
            payload,
            cancellation
            );
        response.EnsureSuccessStatusCode();
        var location = response.Headers.Location;
        if (location is null)
            throw new InvalidOperationException("Keycloak did not return the user location header.");
        return location.Segments.Last();
    }

    public async Task AssignRealmRoleAsync(string KeycloakUserId, string roleName, CancellationToken cancellation = default)
    {
        var accessToken = await _keycloakTokenProvider.GetAdminAccessTokenAsync(cancellation);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
        var role = await GetRealmRoleAsync(roleName, cancellation);
        var payload = new[]
        {
            new
            {
                id=role.Id,
                name=role.Name,
            }
        };
        var response = await _httpClient.PostAsJsonAsync(
            $"/admin/realms/{_options.Realm}/users/{KeycloakUserId}/role-mappings/realm",
            payload,
            cancellation
            );
        response.EnsureSuccessStatusCode();
    }
    public async Task DeleteUserAsync(string KeycloakUserId, CancellationToken cancellationToken)
    {
        var accessToken = await _keycloakTokenProvider.GetAdminAccessTokenAsync(cancellationToken);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.DeleteAsync(
            $"/admin/realms/{_options.Realm}/users/{KeycloakUserId}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    private async Task<KeycloakRole> GetRealmRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(
            $"/admin/realms/{_options.Realm}/roles/{roleName}",
             cancellationToken
            );
        response.EnsureSuccessStatusCode();
        var role = await response.Content.ReadFromJsonAsync<KeycloakRole>(cancellationToken);
        return role ?? throw new InvalidOperationException($"Role '{roleName}' was not found.");
    }

    public async Task<bool> RealmExistsAsync(string realmName)
    {
        var accessToken =
        await _keycloakTokenProvider.GetAdminAccessTokenAsync();

        _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", accessToken);

        var response =
        await _httpClient.GetAsync(
        $"/admin/realms/{realmName}");

        return response.IsSuccessStatusCode;
    }

    public async Task CreateRealmAsync(string realmName)
    {
        var accessToken =
            await _keycloakTokenProvider.GetAdminAccessTokenAsync();

        _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", accessToken);

        var response =
        await _httpClient.PostAsJsonAsync(
        "/admin/realms",
        new
        {
            realm = realmName,
            enabled = true
        });

        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> RoleExistsAsync(string realmName, string roleName)
    {
        var accessToken =
          await _keycloakTokenProvider.GetAdminAccessTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

            var response =
            await _httpClient.GetAsync(
            $"/admin/realms/{realmName}/roles/{roleName}");

            return response.IsSuccessStatusCode;
    }

    public async Task CreateRoleAsync(string realmName, string roleName)
    {
        var accessToken =
        await _keycloakTokenProvider.GetAdminAccessTokenAsync();

        _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", accessToken);

        var response =
        await _httpClient.PostAsJsonAsync(
        $"/admin/realms/{realmName}/roles",
        new
        {
            name = roleName
        });

        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> ClientExistsAsync(string realmName, string clientId)
    {
        var accessToken =
        await _keycloakTokenProvider.GetAdminAccessTokenAsync();

        _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", accessToken);

        var response =
        await _httpClient.GetAsync(
        $"/admin/realms/{realmName}/clients?clientId={clientId}");

        response.EnsureSuccessStatusCode();

        var clients =
        await response.Content
        .ReadFromJsonAsync<List<object>>();

        return clients is { Count: > 0 };
    }

    public async Task CreateClientAsync(string realmName, string clientId, string secret)
    {
        var accessToken =
         await _keycloakTokenProvider.GetAdminAccessTokenAsync();

        _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", accessToken);

        var payload = new
        {
            clientId,
            enabled = true,
            publicClient = false,
            serviceAccountsEnabled = true,
            secret
        };

        var response =
        await _httpClient.PostAsJsonAsync(
        $"/admin/realms/{realmName}/clients",
        payload);

        response.EnsureSuccessStatusCode();
    }

}
