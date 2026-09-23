using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.DTOs;


public sealed class CreateKeycloakUserRequest
{
    public string Username { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public bool Enabled { get; init; } = true;

    public bool EmailVerified { get; init; } = false;
}