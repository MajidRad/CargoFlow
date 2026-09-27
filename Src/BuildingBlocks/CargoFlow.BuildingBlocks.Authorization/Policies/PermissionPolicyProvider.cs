using CargoFlow.BuildingBlocks.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;


namespace CargoFlow.BuildingBlocks.Authorization.Policies;

public sealed class PermissionPolicyProvider
    : DefaultAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(
        IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }

    public override Task<AuthorizationPolicy?> GetPolicyAsync(
        string policyName)
    {
        var policy = new AuthorizationPolicyBuilder()
            .AddRequirements(
                new PermissionRequirement(policyName))
            .Build();

        return Task.FromResult<AuthorizationPolicy?>(
            policy);
    }
}