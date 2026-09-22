namespace CargoFlow.BuildingBlocks.Domain;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; }

    public DateTime OccurredOnUtc { get; }
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOnUtc = DateTime.UtcNow;
    }
    protected DomainEvent(Guid id, DateTime occurredOnUtc)
    {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
    }
}

