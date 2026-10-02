using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Abstractions.Authentication;
using CargoFlow.Identity.Application.Abstractions.Persistence;
using CargoFlow.Identity.Application.DTOs;
using CargoFlow.Identity.Domain.Repositories;

using ErrorOr;


namespace CargoFlow.Identity.Application.Features.Authentication.RefreshToken;

public sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork
        )
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _refreshTokenGenerator = refreshTokenGenerator;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(
            request.refreshToken,
            cancellationToken);
        if (user == null) return Error.Forbidden();

        var currentToken = user.RefreshTokens
            .FirstOrDefault(x=>x.Token==request.refreshToken);

        if(currentToken.Revoked)
            return Error.Unauthorized("RefreshToken.Notvalid","RefreshToken is revoked");

        if(currentToken.IsExpired())
            return Error.Unauthorized("RefreshToken.Notvalid", "RefreshToken is expired");

        currentToken.Revoke();

        var newRefreshToken = _refreshTokenGenerator.Generate();
        user.AddRefreshToken(
            new Domain.Entities.RefreshToken(
                Guid.NewGuid(),
                newRefreshToken,
                DateTime.UtcNow.AddDays(7)));

        var roles = user.Roles.Select(x => x.Name);
        var permissions = user.Roles
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .Distinct();

        var accessToken = _jwtProvider.GenerateAccessToken(user.Id, user.Email.Value, roles, permissions);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new AuthResponse(accessToken, newRefreshToken);
    }
}