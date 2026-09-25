using CargoFlow.BuildingBlocks.Presentation.Extensions;
using CargoFlow.Identity.Application.Users.Commands;
using Carter;
using MediatR;

namespace CargoFlow.Identity.Api.Endpoints.Users;

public class CreateUserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users", async (CreateUserRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateUserCommand(
                                request.Email,
                                request.FirstName,
                                request.LastName,
                                request.Password);
            var result = await sender.Send(command, cancellationToken);

            return result.Match(
               id => Results.Created(
               $"/api/users/{id}",
               new { Id = id }),
               errors => errors.ToProblem());
    });

    }
}
public sealed record CreateUserRequest(
    string Email,
    string FirstName,
    string LastName,
    string Password);