using CargoFlow.Identity.Application.Abstractions.Authentication;
using CargoFlow.Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CargoFlow.Identity.Infrastructure.Authentication;

public sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    private ClaimsPrincipal User=>
        _httpContextAccessor.HttpContext?.User??throw new UnauthorizedAccessException();
    public Guid UserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public string Email => User.FindFirstValue(ClaimTypes.Email)!;

    public IReadOnlyCollection<string> Roles => User
        .FindAll(ClaimTypes.Role)
        .Select(x=>x.Value)
        .ToList();

    public IReadOnlyCollection<string> Permissions => User
        .FindAll("permission")
        .Select(x => x.Value)
        .ToList();
}
