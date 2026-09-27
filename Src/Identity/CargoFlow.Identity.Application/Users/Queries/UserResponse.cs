namespace CargoFlow.Identity.Application.Users.Queries;

public sealed record UserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);
