using Carter;
using MediatR;

namespace CargoFlow.Identity.Api.Endpoints.Roles;

public sealed class AssignRoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users/{userId:guid}/roles",
        async (
        Guid userId,
        AssignRoleRequest request,
        ISender sender,
        CancellationToken cancellationToken) =>
        {
            var command = new AssignRoleCommand(
    userId,
    request.RoleName);

            var result = await sender.Send(
    command,
    cancellationToken);

            return result.Match(
    _ => Results.NoContent(),
    errors => errors.ToProblem());
        });
    }
}

public sealed record AssignRoleRequest(
string RoleName);