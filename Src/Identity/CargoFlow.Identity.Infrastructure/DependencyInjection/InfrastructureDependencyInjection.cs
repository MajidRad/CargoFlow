using CargoFlow.Identity.Application.Abstractions.Authentication;
using CargoFlow.Identity.Application.Abstractions.Persistence;
using CargoFlow.Identity.Application.Abstractions.Security;
using CargoFlow.Identity.Domain.Repositories;
using CargoFlow.Identity.Infrastructure.Authentication;
using CargoFlow.Identity.Infrastructure.Authorization;
using CargoFlow.Identity.Infrastructure.Persistence;
using CargoFlow.Identity.Infrastructure.Persistence.Repositories;
using CargoFlow.Identity.Infrastructure.Persistence.Seeding;
using CargoFlow.Identity.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CargoFlow.Identity.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>{
            options
            .UseNpgsql(configuration.GetConnectionString("IdentityDatabase"));
        });
        
        services.Configure<JwtOptions>(
            configuration.GetSection("Jwt"));

        services.Configure<AdminUserOptions>(
            configuration.GetSection("AdminUser"));

        services.AddScoped<IUnitOfWork>(sp=>sp.GetRequiredService<IdentityDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();


        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddSingleton<IRefreshTokenGenerator,
            RefreshTokenGenerator>();

        services.AddSingleton<
            IAuthorizationHandler,
            PermissionAuthorizationHandler>();

        services.AddHttpContextAccessor();

        return services;
    }
}
