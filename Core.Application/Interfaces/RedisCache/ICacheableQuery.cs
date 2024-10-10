using System;

namespace Core.Application.Interfaces.RedisCache;

public interface ICacheableQuery
{
    string CacheKey { get; }

    double CacheTime { get; }
}
