using CargoFlow.Identity.Domain.Entities;
using CargoFlow.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository:IUserRepository
{
    private readonly IdentityDbContext _dbContext;

    public UserRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(
    UserId id,
    CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(
        x => x.Id == id,
        cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
    Email email,
    CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(
        x => x.Email == email,
        cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
    Email email,
    CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
        .AnyAsync(
        x => x.Email == email,
        cancellationToken);
    }

    public async Task AddAsync(
    User user,
    CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(
        user,
        cancellationToken);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }
}
