using CargoFlow.BuildingBlocks.Domain;
using CargoFlow.Identity.Domain.ValueObjects;

namespace CargoFlow.Identity.Domain.Entities;

public class AuditLog:Entity<Guid>
{
    public Guid Id { get; private set; }    
    public UserId UserId { get; private set; }
    public string Action {  get; private set; }
    public DateTime TimeStamp { get; private set; } 
    public AuditLog(UserId userId, string action)
    {
        Id=Guid.NewGuid();
        UserId=userId;
        Action=action;
        TimeStamp = DateTime.UtcNow;
    }
    
}