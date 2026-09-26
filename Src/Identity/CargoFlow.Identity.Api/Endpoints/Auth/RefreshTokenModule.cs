using Carter;
using MediatR;

namespace CargoFlow.Identity.Api.Endpoints.Auth;

public sealed class RefreshTokenModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/refresh",
            async (
                RefreshTokenRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new RefreshTokenCommand(
                    request.RefreshToken);

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

public sealed record RefreshTokenRequest(
    string RefreshToken);
