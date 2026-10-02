using CargoFlow.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Domain.Entities;

public class Permission : Entity<Guid>
{
    private Permission()
    {
    }

    public string Name { get; private set; }

    public Permission(
    Guid id,
    string name)
    : base(id)
    {
        Name = name;
    }
    public static Permission Create(
        Guid id,
        string name)
    {
        return new Permission(
            id,
            name);
    }
}
