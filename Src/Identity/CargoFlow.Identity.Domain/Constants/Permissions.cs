using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Domain.Constants;



public static class Permissions
{
    public const string UsersRead =
        "users:read";

    public const string UsersWrite =
        "users:write";

    public const string ShipmentCreate =
        "shipment:create";

    public const string ShipmentUpdate =
        "shipment:update";

    public const string ShipmentDelete =
        "shipment:delete";

    public const string WarehouseManage =
        "warehouse:manage";

    public const string ViewOwnShipments =
        "shipment:view-own";
}
