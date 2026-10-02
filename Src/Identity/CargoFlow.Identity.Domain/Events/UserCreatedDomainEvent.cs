using CargoFlow.BuildingBlocks.Domain;
using CargoFlow.Identity.Domain.Aggregate;
using CargoFlow.Identity.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Domain.Events;


public sealed class UserCreatedDomainEvent
    : DomainEvent
{
    public Guid UserId { get; }

    public UserCreatedDomainEvent(Guid userId)
    {
        UserId = userId;
    }
}
