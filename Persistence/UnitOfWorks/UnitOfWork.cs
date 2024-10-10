using System;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UnitOfWorks;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();

    public int SaveChanges() => dbContext.SaveChanges();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await dbContext.SaveChangesAsync(cancellationToken);
    
    IReadRepository<TEntity> IUnitOfWork.GetReadRepository<TEntity>() => new ReadRepository<TEntity>(dbContext);

    IWriteRepository<TEntity> IUnitOfWork.GetWriteRepository<TEntity>()=> new WriteRepository<TEntity>(dbContext);
}
