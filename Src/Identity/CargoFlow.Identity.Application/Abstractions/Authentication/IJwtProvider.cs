using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    string GenerateAccessToken(
    Guid userId,
    string email,
    IEnumerable<string> roles,
    IEnumerable<string> permissions);
}