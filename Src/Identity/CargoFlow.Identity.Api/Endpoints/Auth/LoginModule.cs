using Carter;
using MediatR;

namespace CargoFlow.Identity.Api.Endpoints.Auth;

public class LoginModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/login", async (LoginRequest request,ISender sender) =>
        {
          //await sender.Send()
        });
    }
}
public sealed record LoginRequest(
    string Username,
    string Password
    );