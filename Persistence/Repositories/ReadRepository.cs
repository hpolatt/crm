using System;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext dbContext;

    public ReadRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    private DbSet<T> Entities {get => dbContext.Set<T>(); }

    public Task<int> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate)
    {
        IQueryable<T> queryAble = Entities;
        queryAble.AsNoTracking();
        if(predicate is not null) queryAble.Where(predicate);

        return queryAble.CountAsync();
        
    }

    public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool enableTracking = false)
    {
        IQueryable<T> queryAble = Entities;
        if(!enableTracking) queryAble.AsNoTracking();
        return queryAble.Where(predicate);
    }

    public async Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
    {
        IQueryable<T> queryAble = Entities;
        if(!enableTracking) queryAble.AsNoTracking();
        if(include is not null) include(queryAble);
        if(predicate is not null) queryAble.Where(predicate);
        if(orderBy is not null) return await orderBy(queryAble).ToListAsync();
        return await queryAble.ToListAsync();
    }

    public async Task<IList<T>> GetAllByAsyncPaging(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
    {
        IQueryable<T> queryAble = Entities;
        if(!enableTracking) queryAble.AsNoTracking();
        if(include is not null) include(queryAble);
        if(predicate is not null) queryAble.Where(predicate);
        if(orderBy is not null) return await orderBy(queryAble).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        return await queryAble.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
    {
        IQueryable<T> queryAble = Entities;
        if(!enableTracking) queryAble.AsNoTracking();
        if(include is not null) include(queryAble);
        return queryAble.FirstOrDefaultAsync(predicate)!;
    }
}
