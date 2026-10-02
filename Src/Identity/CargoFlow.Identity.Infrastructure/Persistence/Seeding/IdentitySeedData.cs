using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Persistence.Seeding;

public static class IdentitySeedData
{
    // Roles

    public static readonly Guid AdminRoleId =
    Guid.Parse("11111111-1111-1111-1111-111111111111");

    public static readonly Guid DispatcherRoleId =
    Guid.Parse("22222222-2222-2222-2222-222222222222");

    public static readonly Guid CustomerRoleId =
    Guid.Parse("33333333-3333-3333-3333-333333333333");

    public static readonly Guid WarehouseStaffRoleId =
    Guid.Parse("44444444-4444-4444-4444-444444444444");

    // Permissions

    public static readonly Guid UsersReadPermissionId =
    Guid.Parse("10000000-0000-0000-0000-000000000001");

    public static readonly Guid UsersWritePermissionId =
    Guid.Parse("10000000-0000-0000-0000-000000000002");

    public static readonly Guid ShipmentCreatePermissionId =
    Guid.Parse("10000000-0000-0000-0000-000000000003");

    public static readonly Guid ShipmentUpdatePermissionId =
    Guid.Parse("10000000-0000-0000-0000-000000000004");

    public static readonly Guid ShipmentDeletePermissionId =
    Guid.Parse("10000000-0000-0000-0000-000000000005");

    public static readonly Guid WarehouseManagePermissionId =
    Guid.Parse("10000000-0000-0000-0000-000000000006");

    public static readonly Guid ViewOwnShipmentPermissionId =
    Guid.Parse("10000000-0000-0000-0000-000000000007");
}
