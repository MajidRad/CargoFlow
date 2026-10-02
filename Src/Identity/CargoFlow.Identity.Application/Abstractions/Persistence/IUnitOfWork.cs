using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken);
}