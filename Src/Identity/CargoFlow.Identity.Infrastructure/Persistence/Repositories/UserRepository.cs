using CargoFlow.Identity.Domain.Aggregate;
using CargoFlow.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;

    public UserRepository(IdentityDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
                     .FirstOrDefaultAsync(
                     x => x.Email.Value == email,
                     cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
              .FirstOrDefaultAsync(
              x => x.Id == id,
              cancellationToken);
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x=>x.Roles)
            .ThenInclude(x=>x.Permissions)
            .Include(x=>x.RefreshTokens)
            .FirstOrDefaultAsync(x=>x.RefreshTokens
            .Any(r=>r.Token==refreshToken),cancellationToken
            );
    }

    public Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);

        return Task.CompletedTask;
    }
}
