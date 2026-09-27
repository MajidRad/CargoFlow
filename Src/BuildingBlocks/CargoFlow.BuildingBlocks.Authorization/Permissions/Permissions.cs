using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.BuildingBlocks.Authorization.Permissions;

public static class Permissions
{
    public static class Users
    {
        public const string Create = "users:create";
        public const string View = "users:view";
        public const string Update = "users:update";
        public const string Delete = "users:delete";
    }

    public static class Roles
    {
        public const string Assign = "roles:assign";
        public const string View = "roles:view";
    }

    public static class Orders
    {
        public const string Create = "orders:create";
    }
}
