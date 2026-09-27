using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Interfaces;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using ErrorOr;

namespace CargoFlow.Identity.Application.Roles.Commands;

internal sealed class AssignRoleCommandHandler
    : ICommandHandler<AssignRoleCommand, Success>
{
    private readonly IUserRepository _userRepository;
    private readonly IKeycloakRoleService _roleService;

    public AssignRoleCommandHandler(
        IUserRepository userRepository,
        IKeycloakRoleService roleService)
    {
        _userRepository = userRepository;
        _roleService = roleService;
    }

    public async Task<ErrorOr<Success>> Handle(
        AssignRoleCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            new UserId(request.UserId),
            cancellationToken);

        if (user is null)
        {
            return Error.NotFound(
                "User.NotFound",
                "User not found.");
        }

        await _roleService.AssignRoleAsync(
            user.KeyCloakId,
            request.RoleName,
            cancellationToken);

        return Result.Success;
    }
}