using CargoFlow.Identity.Application.Auth.Commands;
using CargoFlow.Identity.Application.Interfaces;
using CargoFlow.Identity.Infrastructure.Keycloak.Clients;
using ErrorOr;


namespace CargoFlow.Identity.Infrastructure.Keycloak.Services;

internal sealed class AuthService : IAuthService
{
    private readonly IKeycloakAuthClient _keycloakAuthClient;

    public AuthService(
    IKeycloakAuthClient keycloakAuthClient)
    {
        _keycloakAuthClient = keycloakAuthClient;
    }

    public async Task<ErrorOr<LoginResponse>> LoginAsync(
    string username,
    string password,
    CancellationToken cancellationToken = default)
    {
        try
        {
            var token = await _keycloakAuthClient.LoginAsync(
            username,
            password,
            cancellationToken);

            if (token is null)
            {
                return Error.Unauthorized(
                "Auth.InvalidCredentials",
                "Invalid username or password.");
            }

            return new LoginResponse(
            token.AccessToken,
            token.RefreshToken,
            token.ExpiresIn);
        }
        catch
        {
            return Error.Unauthorized(
            "Auth.InvalidCredentials",
            "Invalid username or password.");
        }
    }

    public async Task<ErrorOr<LoginResponse>> RefreshAsync(
    string refreshToken,
    CancellationToken cancellationToken = default)
    {
        try
        {
            var token = await _keycloakAuthClient.RefreshTokenAsync(
            refreshToken,
            cancellationToken);

            if (token is null)
            {
                return Error.Unauthorized(
                "Auth.InvalidRefreshToken",
                "Invalid refresh token.");
            }

            return new LoginResponse(
            token.AccessToken,
            token.RefreshToken,
            token.ExpiresIn);
        }
        catch
        {
            return Error.Unauthorized(
            "Auth.InvalidRefreshToken",
            "Invalid refresh token.");
        }
    }
}