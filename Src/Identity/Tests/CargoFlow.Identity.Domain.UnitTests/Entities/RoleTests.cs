using CargoFlow.Identity.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Domain.UnitTests.Entities;



public class RoleTests
{
    [Fact]
    public void Create_Should_Create_Role()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var role =
            Role.Create(
                id,
                "Admin");

        // Assert
        role.Id.Should().Be(id);

        role.Name.Should().Be("Admin");
    }

    [Fact]
    public void AddPermission_Should_Add_Permission()
    {
        // Arrange
        var role =
            Role.Create(
                Guid.NewGuid(),
                "Admin");

        var permission =
            Permission.Create(
                Guid.NewGuid(),
                "Users.Read");

        // Act
        role.AddPermission(permission);

        // Assert
        role.Permissions.Should()
            .Contain(permission);
    }

    [Fact]
    public void AddPermission_Should_Not_Add_Duplicate()
    {
        // Arrange
        var role =
            Role.Create(
                Guid.NewGuid(),
                "Admin");

        var permission =
            Permission.Create(
                Guid.NewGuid(),
                "Users.Read");

        // Act
        role.AddPermission(permission);
        role.AddPermission(permission);

        // Assert
        role.Permissions.Should()
            .HaveCount(1);
    }
}
