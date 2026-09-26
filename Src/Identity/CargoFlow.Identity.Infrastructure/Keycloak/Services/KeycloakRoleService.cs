using CargoFlow.Identity.Application.Interfaces;
using CargoFlow.Identity.Infrastructure.Keycloak.Clients;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Services;

public sealed class KeycloakRoleService : IKeycloakRoleService
{
    private readonly IkeycloakAdminClient _keycloakAdminClient;

    public KeycloakRoleService(
        IkeycloakAdminClient keycloakAdminClient)
    {
        _keycloakAdminClient = keycloakAdminClient;
    }

    public async Task AssignRoleAsync(
        string keycloakUserId,
        string roleName,
        CancellationToken cancellationToken = default)
    {
        await _keycloakAdminClient.AssignRealmRoleAsync(
            keycloakUserId,
            roleName,
            cancellationToken);
    }
}