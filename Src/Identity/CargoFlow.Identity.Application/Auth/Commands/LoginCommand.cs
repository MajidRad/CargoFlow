using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.Interfaces;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Auth.Commands;

public sealed record LoginCommand(
    string Username,
    string Password)
    : ICommand<LoginResponse>;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn);

internal sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<ErrorOr<LoginResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        return _authService.LoginAsync(
            request.Username,
            request.Password,
            cancellationToken);
    }
}