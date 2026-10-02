using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Application.DTOs;


namespace CargoFlow.Identity.Application.Features.Authentication.RefreshToken;

public sealed record RefreshTokenCommand(
string refreshToken)
: ICommand<AuthResponse>;
