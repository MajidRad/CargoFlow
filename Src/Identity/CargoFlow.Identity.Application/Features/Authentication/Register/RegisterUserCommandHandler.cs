using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Abstractions.Persistence;
using CargoFlow.Identity.Application.Abstractions.Security;
using CargoFlow.Identity.Domain.Aggregate;
using CargoFlow.Identity.Domain.Repositories;
using CargoFlow.Identity.Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace CargoFlow.Identity.Application.Features.Authentication.Register;

public sealed class RegisterUserCommandHandler
    : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IUserRepository users,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _users = users;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser =
         await _users.GetByEmailAsync(
             request.Email,
             cancellationToken);

        if (existingUser is not null)
            throw new Exception("Email already exists.");

        var hash =
            _passwordHasher.Hash(
                request.Password);

        var user = User.Create(
            FullName.Create(
                request.FirstName,
                request.LastName),
            Email.Create(request.Email),
            PasswordHash.Create(hash));

        await _users.AddAsync(
            user,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return user.Id;
    }
}