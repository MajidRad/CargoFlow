using CargoFlow.BuildingBlocks.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Users.Queries;
public sealed record GetUserByIdQuery(
    Guid UserId)
    : IQuery<UserResponse>;
