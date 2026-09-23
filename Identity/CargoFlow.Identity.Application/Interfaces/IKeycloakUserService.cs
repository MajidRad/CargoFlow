using CargoFlow.Identity.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Interfaces;


public interface IKeycloakUserService
{
    Task<string> CreateUserAsync(
        CreateKeycloakUserRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteUserAsync(
        string keycloakUserId,
        CancellationToken cancellationToken = default);
}
