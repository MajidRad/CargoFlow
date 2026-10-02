using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.DTOs;

public sealed record AuthResponse(
    string AccessToken,
    string RefreshToken);
