using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Models;

public sealed class KeycloakUserRepresentation
{
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public bool Enabled { get; init; }
    public bool EmailVerified { get; init; }
    public ICollection<KeycloakCredential> Credentials { get; init; } = [];

}
public sealed class KeycloakCredential
{
    public string Type { get; init; } = "password";
    public string Value { get; init; } = string.Empty;
    public bool Temporary { get; init; }
}