using CargoFlow.BuildingBlocks.Domain;
using CargoFlow.Identity.Domain.ValueObjects;

namespace CargoFlow.Identity.Domain.Events;

public sealed class UserCreatedDomainEvent : DomainEvent
{
    public UserId UserId { get; private set; }
    public Email Email { get; private set; }
    public FullName FullName { get; set; }
    public string KeycloakId {  get; private set; }
    public UserCreatedDomainEvent(
         UserId userId,
         Email email,
         FullName fullName,
         string keycloakId)
    {
        UserId = userId;
        Email = email;
        FullName = fullName;
        KeycloakId = keycloakId;
    }

}