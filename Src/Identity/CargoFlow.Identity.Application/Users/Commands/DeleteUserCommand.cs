using CargoFlow.BuildingBlocks.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Users.Commands;

public sealed record DeleteUserCommand(Guid UserId)
    : ICommand<Deleted>;
public sealed record Deleted;
