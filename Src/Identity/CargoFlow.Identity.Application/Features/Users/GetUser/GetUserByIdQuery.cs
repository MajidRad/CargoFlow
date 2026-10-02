using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.DTOs;
using CargoFlow.Identity.Domain.Repositories;
using ErrorOr;

namespace CargoFlow.Identity.Application.Features.Users.GetUser;

public record GetUserByIdQuery(Guid UserId)
: IQuery<UserResponse>;

public class GetUserByIdQueryHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{

    public async Task<ErrorOr<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null) return Error.NotFound($"user{request.UserId} was not found");

        var roles = user.Roles.Select(r => r.Name).ToList();
        var permissions = user.Roles
            .SelectMany(r => r.Permissions)
            .Select(p => p.Name)
            .Distinct()
            .ToList();

        return new UserResponse(
            user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Email.Value,
            user.IsActive,
            roles,
            permissions);

    }
}