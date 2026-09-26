using CargoFlow.Identity.Application.DTOs;
using CargoFlow.Identity.Application.Interfaces;
using CargoFlow.Identity.Infrastructure.Keycloak.Clients;
using CargoFlow.Identity.Infrastructure.Keycloak.Models;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Services;



public sealed class KeycloakUserService : IKeycloakUserService
{
    private readonly IkeycloakAdminClient _keycloakAdminClient;

    public KeycloakUserService(
        IkeycloakAdminClient keycloakAdminClient)
    {
        _keycloakAdminClient = keycloakAdminClient;
    }

    public async Task<string> CreateUserAsync(CreateKeycloakUserRequest request, CancellationToken cancellationToken = default)
    {
        var keycloakRequest = new CreateKeycloakUserRequest
        {
            Username = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password
        };

        return await _keycloakAdminClient.CreateUserAsync(
            keycloakRequest,
            cancellationToken);
    }


    public async Task DeleteUserAsync(
        string keycloakUserId,
        CancellationToken cancellationToken = default)
    {
        await _keycloakAdminClient.DeleteUserAsync(
            keycloakUserId,
            cancellationToken);
    }
}
