using CargoFlow.Identity.Application.DTOs;
using CargoFlow.Identity.Infrastructure.Keycloak.Models;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Clients;

public interface IkeycloakAdminClient
{
    Task<string> CreateUserAsync(
        CreateKeycloakUserRequest request,
        CancellationToken cancellation = default
        );

    Task AssignRealmRoleAsync(
        string KeycloakUserId,
        string roleName,
        CancellationToken cancellation = default
        );

    Task DeleteUserAsync(
        string KeycloakUserId,
        CancellationToken cancellationToken = default
        );

    Task<bool> RealmExistsAsync(string realmName);
    Task CreateRealmAsync(string realmName);

    Task<bool> RoleExistsAsync(string realmName, string roleName);
    Task CreateRoleAsync(string realmName, string roleName);

    Task<bool> ClientExistsAsync(string realmName, string clientId);
    Task CreateClientAsync(
    string realmName,
    string clientId,
    string secret);

}