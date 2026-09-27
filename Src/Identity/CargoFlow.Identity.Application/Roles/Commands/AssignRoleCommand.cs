using CargoFlow.BuildingBlocks.Application.Abstractions;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Roles.Commands;

public sealed record AssignRoleCommand(
    Guid UserId,
    string RoleName)
    : ICommand<Success>;
