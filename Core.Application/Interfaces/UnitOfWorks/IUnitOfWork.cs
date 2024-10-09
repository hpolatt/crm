using System;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Common;

namespace Core.Application.Interfaces.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{

    IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class, IEntityBase, new();
    IWriteRepository<TEntity> GetWriteRepository<TEntity>() where TEntity : class, IEntityBase, new();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    int SaveChanges();



}
