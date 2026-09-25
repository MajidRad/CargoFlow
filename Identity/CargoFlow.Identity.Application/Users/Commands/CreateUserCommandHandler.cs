using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Abstractions;
using CargoFlow.Identity.Application.DTOs;
using CargoFlow.Identity.Application.Interfaces;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using ErrorOr;

namespace CargoFlow.Identity.Application.Users.Commands;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IKeycloakUserService _userService;
    private readonly IKeycloakRoleService _roleService;

    public CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IKeycloakUserService userService,
    IKeycloakRoleService roleService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _userService = userService;
        _roleService = roleService;
    }
    public async Task<ErrorOr<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var exists = await _userRepository.ExistsByEmailAsync(email, cancellationToken);
        if (exists)
        {
            return Error.Conflict("User.EmailExist", "User Already Exist");
        }
        var keycloakUserId = await _userService.CreateUserAsync(
                        new CreateKeycloakUserRequest
                        {
                            Email = request.Email,
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            Username = request.Email,
                            Password = request.Password,
                            EmailVerified = true,
                            Enabled = true
                        }, cancellationToken);
        await _roleService.AssignRoleAsync(keycloakUserId, "User", cancellationToken);
        var user = new User(
                    UserId.New(),
                    email,
                    FullName.Create(
                    request.FirstName,
                    request.LastName),
                    keycloakUserId);
        await _userRepository.AddAsync(user: user, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return user.Id.Value;
    }
}