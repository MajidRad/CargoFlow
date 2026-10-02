using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Abstractions.Persistence;
using CargoFlow.Identity.Domain.Repositories;
using ErrorOr;

namespace CargoFlow.Identity.Application.Features.Authentication.Logout;

public sealed class LogoutCommandHandler
    : ICommandHandler<LogoutCommand,Success>
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutCommandHandler(
        IUserRepository users,
        IUnitOfWork unitOfWork)
    {
        _users = users;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user =
           await _users.GetByRefreshTokenAsync(
               request.RefreshToken,
               cancellationToken);

        if (user is null)
            return Error.Unauthorized();

        var token =
            user.RefreshTokens
                .FirstOrDefault(x =>
                    x.Token == request.RefreshToken);

        token?.Revoke();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);
        return new ErrorOr<Success>();
    }
}