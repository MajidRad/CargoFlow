using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Abstractions;
using CargoFlow.Identity.Application.Interfaces;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using ErrorOr;

namespace CargoFlow.Identity.Application.Users.Commands;

internal sealed class DeleteUserCommandHandler
    : ICommandHandler<DeleteUserCommand, Deleted>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IKeycloakUserService _userService;

    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IKeycloakUserService userService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task<ErrorOr<Deleted>> Handle(
        DeleteUserCommand request,
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

        await _userService.DeleteUserAsync(
            user.KeyCloakId,
            cancellationToken);

        _userRepository.Delete(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Deleted();
    }
}