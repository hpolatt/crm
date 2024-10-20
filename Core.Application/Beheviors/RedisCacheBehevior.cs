using System;
using Core.Application.Interfaces.RedisCache;
using MediatR;

namespace Core.Application.Beheviors;

public class RedisCacheBehevior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IRedisCacheService redisCacheService;

    public RedisCacheBehevior(IRedisCacheService redisCacheService)
    {
        this.redisCacheService = redisCacheService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICacheableQuery query) return await next();

        var cacheKey = query.CacheKey;
        var cacheTime = query.CacheTime;

        var cachedResponse = await redisCacheService.GetAsync<TResponse>(cacheKey);
        if (cachedResponse is not null) return cachedResponse;

        var response = await next();
        if (response is not null) await redisCacheService.SetAsync(cacheKey, response, DateTime.Now.AddMinutes(cacheTime));

        return response;
    }
}
