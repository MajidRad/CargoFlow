using CargoFlow.BuildingBlocks.Domain;
using CargoFlow.Identity.Domain.Events;
using CargoFlow.Identity.Domain.ValueObjects;

namespace CargoFlow.Identity.Domain.Entities;

public class User : AggregateRoot<UserId>
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public FullName FullName { get; private set; }
    public string KeyCloakId { get; private set; }

    public UserMetadata UserMetadata { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public User(UserId id, Email email, FullName fullName, string keycloakId)
    {
        Id = id;
        Email = email;
        FullName = fullName;
        KeyCloakId = keycloakId;
        UserMetadata = UserMetadata.CreateDefault();
        CreatedAt = DateTime.UtcNow;
        Raise(new UserCreatedDomainEvent(userId:id,email:email,fullName:fullName,keycloakId:keycloakId));
    }
    public void UpdateProfile(FullName fullName)
    {
        FullName = fullName;
        UpdatedAt = DateTime.UtcNow;
        Raise(new UserUpdatedDomainEvent(fullName, updatedAt: UpdatedAt.Value));
    }
}
