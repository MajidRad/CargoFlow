namespace CargoFlow.Identity.Api.Endpoints.Users;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName);