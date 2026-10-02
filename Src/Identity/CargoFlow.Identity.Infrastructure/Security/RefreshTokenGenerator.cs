using CargoFlow.Identity.Application.Abstractions.Authentication;
using System.Security.Cryptography;



namespace CargoFlow.Identity.Infrastructure.Security;

public sealed class RefreshTokenGenerator
: IRefreshTokenGenerator
{
    public string Generate()
    {
        return Convert.ToBase64String(
        RandomNumberGenerator.GetBytes(64));
    }
}