using CargoFlow.Identity.Infrastructure.Keycloak.Models;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Clients;

public interface IKeycloakAuthClient
{
    Task<TokenResponse?>LoginAsync(
        string username,
        string password,
        CancellationToken cancellation=default);
    Task<TokenResponse?>RefreshTokenAsync(
        string refreshToken,
        CancellationToken tokenCancellation=default 
        );
}
