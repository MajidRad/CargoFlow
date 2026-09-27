using CargoFlow.Identity.Application.Auth.Commands;
using ErrorOr;

namespace CargoFlow.Identity.Application.Interfaces;


public interface IAuthService
{
    Task<ErrorOr<LoginResponse>> LoginAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default);

    Task<ErrorOr<LoginResponse>> RefreshAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}