using System;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Common;
using Persistence.Context;

namespace Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext dbContext;

    public ReadRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public Task<int> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool enableTracking = false)
    {
        throw new NotImplementedException();
    }

    public Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
    {
        throw new NotImplementedException();
    }

    public Task<IList<T>> GetAllByAsyncPaging(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
    {
        throw new NotImplementedException();
    }
}
