using CargoFlow.BuildingBlocks.Domain;

namespace CargoFlow.Identity.Domain.Entities;

public class Role : Entity<Guid>
{
    private readonly List<Permission> _permissions = new();

    private Role()
    {
    }

    public string Name { get; private set; }

    public IReadOnlyCollection<Permission> Permissions
    => _permissions;

    public Role(
    Guid id,
    string name)
    : base(id)
    {
        Name = name;
    }
    public static Role Create(
    Guid id,
    string name)
    {
        return new Role(
            id,
            name);
    }

    public void AddPermission(
    Permission permission)
    {
        if (_permissions.Any(x => x.Id == permission.Id))
            return;

        _permissions.Add(permission);
    }
}
