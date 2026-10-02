using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Authentication;

public sealed class JwtOptions
{
    public string Issuer { get; init; } = null!;

    public string Audience { get; init; } = null!;

    public string Secret { get; init; } = null!;

    public int ExpirationMinutes { get; init; }
    public int RefreshTokenExpirationDays { get; init; }
}
