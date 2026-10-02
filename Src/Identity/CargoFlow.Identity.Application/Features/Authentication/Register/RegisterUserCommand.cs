using CargoFlow.BuildingBlocks.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CargoFlow.Identity.Application.Features.Authentication.Register;

public sealed record RegisterUserCommand(
string FirstName,
string LastName,
string Email,
string Password)
:ICommand<Guid>;
