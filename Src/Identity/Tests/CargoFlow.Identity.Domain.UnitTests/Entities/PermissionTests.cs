using CargoFlow.Identity.Domain.Entities;
using FluentAssertions;

namespace CargoFlow.Identity.Domain.UnitTests.Entities;

public class PermissionTests
{
    [Fact]
    public void Create_Should_Create_Permission()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var permission =
            Permission.Create(
                id,
                "Users.Create");

        // Assert
        permission.Id.Should().Be(id);

        permission.Name.Should()
            .Be("Users.Create");
    }
}