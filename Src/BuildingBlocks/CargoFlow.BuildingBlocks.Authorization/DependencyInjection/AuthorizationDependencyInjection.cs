using CargoFlow.BuildingBlocks.Authorization.Handlers;
using CargoFlow.BuildingBlocks.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;


namespace CargoFlow.BuildingBlocks.Authorization.DependencyInjection;

public static class AuthorizationDependencyInjection
{
    public static IServiceCollection AddPermissionAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        return services; 
    }
}
