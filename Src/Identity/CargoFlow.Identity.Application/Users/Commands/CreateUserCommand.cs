using CargoFlow.BuildingBlocks.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Users.Commands;

public sealed record CreateUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password)
    : ICommand<Guid>;
