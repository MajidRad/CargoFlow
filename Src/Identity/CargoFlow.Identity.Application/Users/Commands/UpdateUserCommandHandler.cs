using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Abstractions;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using ErrorOr;

namespace CargoFlow.Identity.Application.Users.Commands;

internal sealed class UpdateUserCommandHandler
    : ICommandHandler<UpdateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(
        UpdateUserCommand request,
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

        user.UpdateProfile(
            FullName.Create(
                request.FirstName,
                request.LastName));

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}