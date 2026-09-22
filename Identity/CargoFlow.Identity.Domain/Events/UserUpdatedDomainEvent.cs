using CargoFlow.BuildingBlocks.Domain;
using CargoFlow.Identity.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Domain.Events;

public sealed class UserUpdatedDomainEvent:DomainEvent
{
    public FullName FullName { get; private set; }
    public DateTime UpdatedAt {  get; private set; }
    public UserUpdatedDomainEvent(FullName fullName,DateTime updatedAt)
    {
        FullName= fullName; 
        UpdatedAt= updatedAt;   
    }
}
