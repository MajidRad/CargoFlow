using Carter;
using MediatR;

namespace CargoFlow.Identity.Api.Endpoints.Users;

public sealed class DeleteUserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/users/{id:guid}",
        async (
        Guid id,
        ISender sender,
        CancellationToken cancellationToken) =>
        {
            var command = new DeleteUserCommand(id);

            var result = await sender.Send(
    command,
    cancellationToken);

            return result.Match(
    _ => Results.NoContent(),
    errors => errors.ToProblem());
        });
    }
}