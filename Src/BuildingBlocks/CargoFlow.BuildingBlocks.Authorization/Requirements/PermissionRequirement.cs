using Microsoft.AspNetCore.Authorization;

namespace CargoFlow.BuildingBlocks.Authorization.Requirements;

public sealed record PermissionRequirement(
string Permission)
: IAuthorizationRequirement;
