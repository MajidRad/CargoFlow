using CargoFlow.Identity.Application.Abstractions.Security;
using CargoFlow.Identity.Infrastructure.Persistence.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    public static async Task InitialiseAsync(
    IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context =
        scope.ServiceProvider
        .GetRequiredService<IdentityDbContext>();

        await context.Database.MigrateAsync();

        var passwordHasher =
        scope.ServiceProvider
        .GetRequiredService<IPasswordHasher>();

        var adminOptions =
        scope.ServiceProvider
        .GetRequiredService<IOptions<AdminUserOptions>>();
        

        await IdentitySeeder.SeedAsync(
        context,
        passwordHasher,
        adminOptions);
    }
}