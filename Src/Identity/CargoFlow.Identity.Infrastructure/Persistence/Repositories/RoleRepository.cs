using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CargoFlow.Identity.Infrastructure.Persistence.Repositories;

public sealed class RoleRepository
: IRoleRepository
{
    private readonly IdentityDbContext _context;

    public RoleRepository(
    IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken)
    {
        return await _context.Roles
        .Include(x => x.Permissions)
        .FirstOrDefaultAsync(
        x => x.Id == id,
        cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(
    string name,
    CancellationToken cancellationToken)
    {
        return await _context.Roles
        .Include(x => x.Permissions)
        .FirstOrDefaultAsync(
        x => x.Name == name,
        cancellationToken);
    }

    public async Task<List<Role>> GetByIdsAsync(
    IEnumerable<Guid> ids,
    CancellationToken cancellationToken)
    {
        return await _context.Roles
        .Include(x => x.Permissions)
        .Where(x => ids.Contains(x.Id))
        .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
    Role role,
    CancellationToken cancellationToken)
    {
        await _context.Roles.AddAsync(
        role,
        cancellationToken);
    }
}