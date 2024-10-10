using System;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Common;
using Persistence.Context;

namespace Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext dbContext;

    public WriteRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public Task Add(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(IList<T> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Delete(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Update(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
