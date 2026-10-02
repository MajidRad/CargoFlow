using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.DTOs;
using CargoFlow.Identity.Domain.Aggregate;

namespace CargoFlow.Identity.Application.Features.Authentication.Login;

public sealed record LoginCommand(
    string Email,
    string Password)
    : ICommand<AuthResponse>;
