using CargoFlow.Identity.Domain.Entities;

namespace CargoFlow.Identity.Domain.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<List<Role>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken);

    Task<Role?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken);

    Task AddAsync(
        Role role,
        CancellationToken cancellationToken);


}
