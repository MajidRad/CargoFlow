using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Interfaces;
using ErrorOr;

namespace CargoFlow.Identity.Application.Auth.Commands;

internal sealed class RefreshTokenCommandHandler
    : ICommandHandler<RefreshTokenCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(
        IAuthService authService)
    {
        _authService = authService;
    }

    public Task<ErrorOr<LoginResponse>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        return _authService.RefreshAsync(
            request.RefreshToken,
            cancellationToken);
    }
}