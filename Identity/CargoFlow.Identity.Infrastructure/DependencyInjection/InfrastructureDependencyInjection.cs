using CargoFlow.Identity.Application.Interfaces;
using CargoFlow.Identity.Infrastructure.Keycloak;
using CargoFlow.Identity.Infrastructure.Keycloak.Clients;
using CargoFlow.Identity.Infrastructure.Keycloak.Services;
using CargoFlow.Identity.Infrastructure.Keycloak.TokenProvider;
using CargoFlow.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CargoFlow.Identity.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration)
                 .AddKeycloak(configuration);
        return services;    

    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("IdentityDatabase"));
        });
        return services;
    }
    private static IServiceCollection AddKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KeycloakOptions>(
            configuration.GetSection(KeycloakOptions.SectionName));

        services.AddHttpClient<KeycloakTokenProvider>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddSingleton<IKeycloakTokenProvider>(
            sp => sp.GetRequiredService<KeycloakTokenProvider>());

        services.AddHttpClient<IkeycloakAdminClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });
        services.AddHttpClient<IKeycloakAuthClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });
        services.AddScoped<IKeycloakUserService, KeycloakUserService>();
        services.AddScoped<IKeycloakRoleService, KeycloakRoleService>();
        return services;
    }
}
