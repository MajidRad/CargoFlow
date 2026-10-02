using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Abstractions.Authentication;

public interface IRefreshTokenGenerator
{
    string Generate();
}