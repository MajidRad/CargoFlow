using CargoFlow.BuildingBlocks.Domain;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.Events;
using CargoFlow.Identity.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CargoFlow.Identity.Domain.Aggregate;

public class User : AggregateRoot<Guid>
{
    private readonly List<Role> _roles = new();

    private readonly List<RefreshToken> _refreshTokens = new();

    private User()
    {
    }

    public FullName Name { get; private set; }

    public Email Email { get; private set; }

    public PasswordHash PasswordHash { get; private set; }

    public bool IsActive { get; private set; }

    public IReadOnlyCollection<Role> Roles
        => _roles;

    public IReadOnlyCollection<RefreshToken> RefreshTokens
        => _refreshTokens;
    public void AddRefreshToken(RefreshToken token)
    {
        _refreshTokens.Add(token);
    }
    public static User Create(
    FullName name,
    Email email,
    PasswordHash passwordHash)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            IsActive = true
        };

        user.Raise(
            new UserCreatedDomainEvent(
                user.Id));

        return user;
    }
    public void AssignRole(Role role)
    {
        if (_roles.Any(x => x.Id == role.Id))
            return;

        _roles.Add(role);

        Raise(
            new UserRoleAssignedDomainEvent(
                Id,
                role.Id));
    }

}