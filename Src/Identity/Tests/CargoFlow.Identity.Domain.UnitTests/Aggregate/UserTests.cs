using CargoFlow.Identity.Domain.Aggregate;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Domain.UnitTests.Aggregate;

public class UserTests
{
    [Fact]
    public void Create_Should_Create_User()
    {
        var user = User.Create(
            FullName.Create("Majid", "Karimi"),
            Email.Create("majid@test.com"),
            PasswordHash.Create("hash")
            );
        var role =
            Role.Create(
            Guid.NewGuid(),
            "Administrator");

        user.AssignRole(role);
        user.Roles.Should().Contain(role);
    }

    [Fact]
    public void AssignRole_Should_Not_Add_Duplicate_Role()
    {
        var role =
            Role.Create(Guid.NewGuid(), "Administrator");
            
        var user= User.Create(
                        FullName.Create("Majid", "Karimi"),
                        Email.Create("majid@test.com"),
                        PasswordHash.Create("hash"));
        user.AssignRole(role);
        user.AssignRole(role);
        user.Roles.Should().HaveCount(1);
    }
    [Fact]
    public void AssignRole_Should_Raise_Event()
    {
        // Arrange
        var role =
            Role.Create(
                Guid.NewGuid(),
                "Administrator");

        var user =
            User.Create(
                FullName.Create("Majid", "Karimi"),
                Email.Create("majid@test.com"),
                PasswordHash.Create("hash"));

        // Act
        user.AssignRole(role);

        // Assert
        user.DomainEvents.Should()
            .Contain(x =>
                x.GetType().Name ==
                "UserRoleAssignedDomainEvent");
    }
}
