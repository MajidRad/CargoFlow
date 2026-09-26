using Carter;
using MediatR;

namespace CargoFlow.Identity.Api.Endpoints.Users;

public sealed class GetUsersModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users",
        async (
        ISender sender,
        CancellationToken cancellationToken) =>
        {
            var query = new GetUsersQuery();

            var result = await sender.Send(
    query,
    cancellationToken);

            return result.Match(
    users => Results.Ok(users),
    errors => errors.ToProblem());
        });
    }
}