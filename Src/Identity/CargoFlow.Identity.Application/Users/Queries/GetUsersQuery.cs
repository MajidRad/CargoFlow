using CargoFlow.BuildingBlocks.Application.Abstractions;

namespace CargoFlow.Identity.Application.Users.Queries;

public sealed record GetUsersQuery()
    : IQuery<IReadOnlyList<UserResponse>>;
