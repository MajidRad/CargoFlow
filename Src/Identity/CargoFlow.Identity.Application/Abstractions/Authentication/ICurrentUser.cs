
namespace CargoFlow.Identity.Application.Abstractions.Authentication;

public interface ICurrentUser
{
    Guid UserId { get; }
    string Email { get; }
    IReadOnlyCollection<string> Roles { get; }
    IReadOnlyCollection<string> Permissions { get; }
}
