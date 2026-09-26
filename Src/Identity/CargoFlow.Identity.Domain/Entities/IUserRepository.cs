using CargoFlow.Identity.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Domain.Entities;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(
    UserId id,
    CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(
    Email email,
    CancellationToken cancellationToken = default);

    Task<bool> ExistsByEmailAsync(
    Email email,
    CancellationToken cancellationToken = default);

    Task AddAsync(
    User user,
    CancellationToken cancellationToken = default);

    void Update(User user);

    void Delete(User user);
}