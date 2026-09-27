using CargoFlow.Identity.Domain.Entities;
using Carter;
using System.Security.Claims;

namespace CargoFlow.Identity.Api.Endpoints.Auth;

public class DebugClaim : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/debug/claims", (ClaimsPrincipal user) =>
        {
            var claims = user.Claims
            .Select(c => new
            {
                c.Type,
                c.Value
            });

            return Results.Ok(claims);
        })
        .RequireAuthorization();
    }
}
