using Carter;
using MediatR;

namespace CargoFlow.Identity.Api.Endpoints.Users;

public sealed class UpdateUserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id:guid}",
            async (
                Guid id,
                UpdateUserRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateUserCommand(
                    id,
                    request.FirstName,
                    request.LastName);

                var result = await sender.Send(
                    command,
                    cancellationToken);

                return result.Match(
                    userId => Results.Ok(new { Id = userId }),
                    errors => errors.ToProblem());
            });
    }
}
