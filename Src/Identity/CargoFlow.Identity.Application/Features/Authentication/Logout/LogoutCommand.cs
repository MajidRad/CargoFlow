using CargoFlow.BuildingBlocks.Application.Abstractions;
using ErrorOr;
using MediatR;

namespace CargoFlow.Identity.Application.Features.Authentication.Logout;

public sealed record LogoutCommand(
    string RefreshToken)
    : ICommand<Success>;
