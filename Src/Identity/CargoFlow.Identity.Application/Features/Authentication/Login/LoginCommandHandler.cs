using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Abstractions.Authentication;
using CargoFlow.Identity.Application.Abstractions.Persistence;
using CargoFlow.Identity.Application.Abstractions.Security;
using CargoFlow.Identity.Application.DTOs;
using CargoFlow.Identity.Domain.Repositories;
using ErrorOr;

namespace CargoFlow.Identity.Application.Features.Authentication.Login;

public sealed class LoginCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository  userRepository,
    IJwtProvider jwtProvider ,
    IPasswordHasher passwordHasher,
    IRefreshTokenGenerator refreshTokenGenerator
    ) : ICommandHandler<LoginCommand, AuthResponse>
{
    public async Task<ErrorOr<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(
             request.Email,
             cancellationToken);

        if (user is null)
        {
            return Error.Unauthorized(
                code: "Auth.InvalidCredentials",
                description: "Invalid email or password.");
        }

        var valid = passwordHasher.Verify(
            request.Password,
            user.PasswordHash.Value);

        if (!valid)
        {
            return Error.Unauthorized(
                code: "Auth.InvalidCredentials",
                description: "Invalid email or password.");
        }

        var roles = user.Roles
            .Select(x => x.Name);

        var permissions = user.Roles
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .Distinct();

        var accessToken = jwtProvider.GenerateAccessToken(
            user.Id,
            user.Email.Value,
            roles,
            permissions);

        var refreshToken = refreshTokenGenerator.Generate();

        user.AddRefreshToken(
            new Identity.Domain.Entities.RefreshToken(
                Guid.NewGuid(),
                refreshToken,
                DateTime.UtcNow.AddDays(7)));

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new AuthResponse(
            accessToken,
            refreshToken);
    }
}