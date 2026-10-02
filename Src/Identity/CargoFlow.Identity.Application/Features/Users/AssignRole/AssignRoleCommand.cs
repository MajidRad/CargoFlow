using CargoFlow.BuildingBlocks.Application.Abstractions;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CargoFlow.Identity.Application.Features.Users.AssignRole;

public sealed record AssignRoleCommand(
    Guid UserId,
    Guid RoleId)
    : ICommand<Success>;
