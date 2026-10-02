using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Abstractions.Persistence;
using CargoFlow.Identity.Domain.Repositories;
using ErrorOr;

namespace CargoFlow.Identity.Application.Features.Users.AssignRole;

public sealed class AssignRoleCommandHandler : ICommandHandler<AssignRoleCommand, Success>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork
        )
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Success>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
        user!.AssignRole(role!);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new ErrorOr<Success>();
    }
}