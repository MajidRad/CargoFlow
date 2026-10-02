namespace CargoFlow.Identity.Api.Modules.Auth;

using Carter;
using global::CargoFlow.Identity.Application.Features.Authentication.Login;
using global::CargoFlow.Identity.Application.Features.Authentication.Logout;
using global::CargoFlow.Identity.Application.Features.Authentication.RefreshToken;
using global::CargoFlow.Identity.Application.Features.Authentication.Register;
using MediatR;


public sealed class AuthModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/register", Register);
        group.MapPost("/login", Login);
        group.MapPost("/refresh", RefreshToken);
        group.MapPost("/logout", Logout);
    }

    private static async Task<IResult> Register(
        RegisterUserCommand command,
        ISender sender,
        CancellationToken ct)
    {
        var userId = await sender.Send(command, ct);

        return Results.Created(
            $"/api/users/{userId}",
            new { UserId = userId });
    }

    private static async Task<IResult> Login(
        LoginCommand command,
        ISender sender,
        CancellationToken ct)
    {
        var response =
            await sender.Send(command, ct);

        return Results.Ok(response);
    }

    private static async Task<IResult> RefreshToken(
        RefreshTokenCommand command,
        ISender sender,
        CancellationToken ct)
    {
        var response =
            await sender.Send(command, ct);

        return Results.Ok(response);
    }

    private static async Task<IResult> Logout(
        LogoutCommand command,
        ISender sender,
        CancellationToken ct)
    {
        await sender.Send(command, ct);

        return Results.NoContent();
    }
}

