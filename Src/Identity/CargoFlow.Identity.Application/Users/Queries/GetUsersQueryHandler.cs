using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Domain.Entities;
using ErrorOr;

namespace CargoFlow.Identity.Application.Users.Queries;

internal sealed class GetUsersQueryHandler
    : IQueryHandler<GetUsersQuery, IReadOnlyList<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<UserResponse>>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(
            cancellationToken);

        return users
            .Select(x => new UserResponse(
                x.Id.Value,
                x.Email.Value,
                x.FullName.FirstName,
                x.FullName.LastName))
            .ToList();
    }
}