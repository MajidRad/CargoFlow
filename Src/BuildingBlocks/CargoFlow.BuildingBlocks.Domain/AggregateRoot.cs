using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.BuildingBlocks.Domain;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected AggregateRoot(){}
    protected AggregateRoot(TId id) : base(id) { }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;
    protected void Raise(IDomainEvent @event)=>
        _domainEvents.Add(@event);
    protected void ClearDomainEvnets()
        =>_domainEvents.Clear();
    public IReadOnlyCollection<IDomainEvent> DequeueEvents()
    {
        var events= _domainEvents;
        _domainEvents.Clear();
        return events;
    }
}
