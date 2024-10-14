using System;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext dbContext;

    private DbSet<T> Entities => dbContext.Set<T>();

    public WriteRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task AddAsnyc(T entity, CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(IList<T> entities, CancellationToken cancellationToken = default)
    {
        await Entities.AddRangeAsync(entities, cancellationToken);
    }

    public async Task Delete(T entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Entities.Remove(entity), cancellationToken);
    }

    public async Task Update(T entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => Entities.Update(entity), cancellationToken);
    }
}
