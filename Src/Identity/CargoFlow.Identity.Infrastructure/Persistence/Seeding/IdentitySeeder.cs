using CargoFlow.Identity.Application.Abstractions.Security;
using CargoFlow.Identity.Domain.Aggregate;
using CargoFlow.Identity.Domain.Constants;
using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CargoFlow.Identity.Infrastructure.Persistence.Seeding;

public static class IdentitySeeder
{
    public static async Task SeedAsync(
    IdentityDbContext context,
    IPasswordHasher passwordHasher,
    IOptions<AdminUserOptions> adminUserOptions,
    CancellationToken ct = default)
    {
        await SeedPermissions(context, ct);
        await context.SaveChangesAsync(ct);
        await SeedRoles(context, ct);
        await context.SaveChangesAsync(ct);
        await SeedAdminUser(context, passwordHasher,adminUserOptions.Value,ct);
        await context.SaveChangesAsync(ct);
    }

    private static async Task SeedRoles(
    IdentityDbContext context,
    CancellationToken ct)
    {
        if (await context.Roles.AnyAsync(ct))
            return;

        var permissions =
            await context.Permissions
                .ToListAsync(ct);

        Permission Find(string name)
            => permissions.First(x => x.Name == name);

        var admin =
            Role.Create(
                IdentitySeedData.AdminRoleId,
                SystemRoles.Admin);

        admin.AddPermission(
            Find(Permissions.UsersRead));

        admin.AddPermission(
            Find(Permissions.UsersWrite));

        admin.AddPermission(
            Find(Permissions.ShipmentCreate));

        admin.AddPermission(
            Find(Permissions.ShipmentUpdate));

        admin.AddPermission(
            Find(Permissions.ShipmentDelete));

        admin.AddPermission(
            Find(Permissions.WarehouseManage));

        var dispatcher =
            Role.Create(
                IdentitySeedData.DispatcherRoleId,
                SystemRoles.Dispatcher);

        dispatcher.AddPermission(
            Find(Permissions.ShipmentCreate));

        dispatcher.AddPermission(
            Find(Permissions.ShipmentUpdate));

        var warehouse =
            Role.Create(
                IdentitySeedData.WarehouseStaffRoleId,
                SystemRoles.WarehouseStaff);

        warehouse.AddPermission(
            Find(Permissions.WarehouseManage));

        warehouse.AddPermission(
            Find(Permissions.ShipmentUpdate));

        var customer =
            Role.Create(
                IdentitySeedData.CustomerRoleId,
                SystemRoles.Customer);

        customer.AddPermission(
            Find(Permissions.ViewOwnShipments));

        await context.Roles.AddRangeAsync(
            admin,
            dispatcher,
            warehouse,
            customer);
    }

    private static async Task SeedPermissions(
    IdentityDbContext context,
    CancellationToken ct)
    {
        if (await context.Permissions.AnyAsync(ct))
            return;

        var permissions = new[]
        {
        Permission.Create(
            IdentitySeedData.UsersReadPermissionId,
            Permissions.UsersRead),

        Permission.Create(
            IdentitySeedData.UsersWritePermissionId,
            Permissions.UsersWrite),

        Permission.Create(
            IdentitySeedData.ShipmentCreatePermissionId,
            Permissions.ShipmentCreate),

        Permission.Create(
            IdentitySeedData.ShipmentUpdatePermissionId,
            Permissions.ShipmentUpdate),

        Permission.Create(
            IdentitySeedData.ShipmentDeletePermissionId,
            Permissions.ShipmentDelete),

        Permission.Create(
            IdentitySeedData.WarehouseManagePermissionId,
            Permissions.WarehouseManage),

        Permission.Create(
            IdentitySeedData.ViewOwnShipmentPermissionId,
            Permissions.ViewOwnShipments)
    };

        await context.Permissions.AddRangeAsync(
            permissions,
            ct);
    }


    private static async Task SeedAdminUser(
    IdentityDbContext context,
    IPasswordHasher passwordHasher,
    AdminUserOptions adminOptions,
    CancellationToken ct)
    {
        if (await context.Users.AnyAsync(
                x => x.Email.Value== adminOptions.Email.ToLowerInvariant(),
                ct))
        {
            return;
        }

        var role =
            await context.Roles
                .FirstAsync(
                    x => x.Id == IdentitySeedData.AdminRoleId,
                    ct);

        var admin = User.Create(
            FullName.Create(
                adminOptions.FirstName,
                adminOptions.LastName),
            Email.Create(adminOptions.Email),
            PasswordHash.Create(
                passwordHasher.Hash(
                    adminOptions.Password)));

        admin.AssignRole(role);

        await context.Users.AddAsync(
            admin,
            ct);
    }
}

