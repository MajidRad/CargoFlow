namespace CargoFlow.Identity.Application.Interfaces;

public interface IKeycloakRoleService
{
    Task AssignRoleAsync(
        string keycloakUserId,
        string roleName,
        CancellationToken cancellationToken = default);
}