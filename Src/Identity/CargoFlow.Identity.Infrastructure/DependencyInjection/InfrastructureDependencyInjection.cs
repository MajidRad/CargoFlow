
using CargoFlow.Identity.Application.Abstractions;
using CargoFlow.Identity.Application.Interfaces;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Infrastructure.Keycloak;
using CargoFlow.Identity.Infrastructure.Keycloak.Clients;
using CargoFlow.Identity.Infrastructure.Keycloak.Seeders;
using CargoFlow.Identity.Infrastructure.Keycloak.Services;
using CargoFlow.Identity.Infrastructure.Keycloak.TokenProvider;
using CargoFlow.Identity.Infrastructure.Persistence;
using CargoFlow.Identity.Infrastructure.Persistence.Repositories;
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
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IdentityDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
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

        services.AddHttpClient<IkeycloakAdminClient, KeycloakAdminClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddHttpClient<IKeycloakAuthClient, KeycloakAuthClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddHttpClient<IKeycloakHealthChecker, KeycloakHealthChecker>(
        (sp, client) =>
        {
            var options = sp
                .GetRequiredService<IOptions<KeycloakOptions>>()
                .Value;

            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddScoped<IKeycloakUserService, KeycloakUserService>();
        services.AddScoped<IKeycloakRoleService, KeycloakRoleService>();
        services.AddScoped<IKeycloakSeeder, KeycloakSeeder>();
        return services;
    }
}
