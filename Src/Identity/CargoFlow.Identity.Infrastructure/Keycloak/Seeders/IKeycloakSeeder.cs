using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Seeders;

public interface IKeycloakSeeder
{
    Task SeedAsync();
}
