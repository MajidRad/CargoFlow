using CargoFlow.Identity.Infrastructure.Keycloak.Clients;
using Microsoft.Extensions.Options;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Seeders;

public sealed class KeycloakSeeder : IKeycloakSeeder
{
    private readonly IkeycloakAdminClient _adminClient;
    private readonly KeycloakOptions _options;
    public KeycloakSeeder(
    IkeycloakAdminClient adminClient,IOptions<KeycloakOptions> options)
    {
        _options = options.Value;
        _adminClient = adminClient;
    }

    public async Task SeedAsync()
    {
        const string realm = "cargoflow";

        if (!await _adminClient.RealmExistsAsync(realm))
        {
            await _adminClient.CreateRealmAsync(realm);
        }

        if (!await _adminClient.RoleExistsAsync(realm, "Admin"))
        {
            await _adminClient.CreateRoleAsync(realm, "Admin");
        }

        if (!await _adminClient.RoleExistsAsync(realm, "Customer"))
        {
            await _adminClient.CreateRoleAsync(realm, "Customer");
        }

        if (!await _adminClient.RoleExistsAsync(realm, "WarehouseStaff"))
        {
            await _adminClient.CreateRoleAsync(realm, "WarehouseStaff");
        }

        if (!await _adminClient.RoleExistsAsync(realm, "Dispatcher"))
        {
            await _adminClient.CreateRoleAsync(realm, "Dispatcher");
        }

        if (!await _adminClient.ClientExistsAsync(realm, "cargoflow-api"))
        {
            await _adminClient.CreateClientAsync(
            realm,
            "cargoflow-api",
            _options.ClientSecret);
        }
    }
}