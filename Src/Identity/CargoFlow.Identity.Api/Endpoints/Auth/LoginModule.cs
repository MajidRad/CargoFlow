using Carter;
using MediatR;

namespace CargoFlow.Identity.Api.Endpoints.Auth;

public class LoginModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login",
              async (
                  LoginRequest request,
                  ISender sender,
                  CancellationToken cancellationToken) =>
              {
                  var command = new LoginCommand(
                      request.Username,
                      request.Password);

                  var result = await sender.Send(
                      command,
                      cancellationToken);

                  return result.Match(
                      token => Results.Ok(token),
                      errors => errors.ToProblem());
              })
              .AllowAnonymous();
    }
}
public sealed record LoginRequest(
    string Username,
    string Password
    );


