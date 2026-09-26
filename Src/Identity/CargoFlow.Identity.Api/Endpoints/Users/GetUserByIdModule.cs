using Carter;
using MediatR;

namespace CargoFlow.Identity.Api.Endpoints.Users;


public sealed class GetUserByIdModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/{id:guid}",
            async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery(id);

                var result = await sender.Send(
                    query,
                    cancellationToken);

                return result.Match(
                    user => Results.Ok(user),
                    errors => errors.ToProblem());
            });
    }
}