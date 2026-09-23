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

}