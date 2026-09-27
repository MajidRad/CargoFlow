using CargoFlow.BuildingBlocks.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Auth.Commands;

public sealed record RefreshTokenCommand(
    string RefreshToken)
    : ICommand<LoginResponse>;
