using System;
using Core.Domain.Common;

namespace Core.Application.Interfaces.Repositories;

public interface IWriteRepository<T> where T : class, IEntityBase, new()
{
    Task AddAsnyc(T entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IList<T> entities, CancellationToken cancellationToken = default);

    Task Update(T entity, CancellationToken cancellationToken = default);

    Task Delete(T entity, CancellationToken cancellationToken = default);
}
