using CargoFlow.BuildingBlocks.Domain;

namespace CargoFlow.Identity.Domain.Events;

public sealed class UserRoleAssignedDomainEvent
    : DomainEvent
{
    public Guid UserId { get; }

    public Guid RoleId { get; }

    public UserRoleAssignedDomainEvent(
        Guid userId,
        Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}