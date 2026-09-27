using CargoFlow.BuildingBlocks.Application.Abstractions;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using ErrorOr;

namespace CargoFlow.Identity.Application.Users.Queries;

internal sealed class GetUserByIdQueryHandler
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserResponse>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            new UserId(request.UserId),
            cancellationToken);

        if (user is null)
        {
            return Error.NotFound(
                "User.NotFound",
                "User not found.");
        }

        return new UserResponse(
            user.Id.Value,
            user.Email.Value,
            user.FullName.FirstName,
            user.FullName.LastName);
    }
}