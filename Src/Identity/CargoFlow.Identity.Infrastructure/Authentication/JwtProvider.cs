using CargoFlow.Identity.Application.Abstractions.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateAccessToken(Guid userId, string email, IEnumerable<string> roles, IEnumerable<string> permissions)
    {
        var claims = new List<Claim>
      {
          new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
          new Claim(JwtRegisteredClaimNames.Email,email),
          new Claim (ClaimTypes.NameIdentifier,userId.ToString())
      };
        claims.AddRange(
            roles.Select(role =>
            new Claim(ClaimTypes.Role, role)));

        claims.AddRange(
            permissions.Select(permission =>
            new Claim("permission", permission)
            ));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.Secret));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes),
            signingCredentials: credentials
            );
        return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler()
                    .WriteToken(token);

    }
}
