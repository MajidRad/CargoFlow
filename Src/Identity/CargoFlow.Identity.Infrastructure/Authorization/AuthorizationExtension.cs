using CargoFlow.Identity.Domain.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace CargoFlow.Identity.Infrastructure.Authorization;

public static class AuthorizationExtension
{
    public static IServiceCollection AddPermissionAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            foreach (var permission in typeof(Permissions)
            .GetFields()
            .Select(f => f.GetValue(null)?.ToString())
            .Where(x => x is not null))
            {
                options.AddPolicy(
                permission!,
                policy =>
                {
                    policy.Requirements.Add(
                    new PermissionRequirement(
                    permission!));
                });
            }
        });

        return services;
    }
}