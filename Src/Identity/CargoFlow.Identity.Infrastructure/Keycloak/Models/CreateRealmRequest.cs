namespace CargoFlow.Identity.Infrastructure.Keycloak.Models;

public sealed class CreateRealmRequest
{
    public string Realm { get; set; } = default!;
    public bool Enabled { get; set; } = true;
}
