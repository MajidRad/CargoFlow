using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json;

namespace CargoFlow.Identity.Infrastructure.Keycloak.ClaimsTransformation;


public sealed class KeycloakPermissionClaimsTransformation
    : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(
        ClaimsPrincipal principal)
    {
        if (principal.Identity is not ClaimsIdentity identity)
            return Task.FromResult(principal);

        var permissions = principal
            .FindAll("permission")
            .Select(x => x.Value)
            .ToList();

        foreach (var permission in permissions)
        {
            if (identity.HasClaim(
                    "permission",
                    permission))
            {
                continue;
            }

            identity.AddClaim(
                new Claim(
                    "permission",
                    permission));
        }

        return Task.FromResult(principal);
    }
}