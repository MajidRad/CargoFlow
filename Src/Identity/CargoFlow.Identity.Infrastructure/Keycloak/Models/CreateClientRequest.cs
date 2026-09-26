namespace CargoFlow.Identity.Infrastructure.Keycloak.Models;

public sealed class CreateClientRequest
{
    public string ClientId { get; set; } = default!;

    public bool Enabled { get; set; } = true;

    public bool PublicClient { get; set; }

    public bool ServiceAccountsEnabled { get; set; }

    public string Secret { get; set; } = default!;
}
