using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Mapper;

public static class Registration
{
    public static void AddCustomMapper(this IServiceCollection services)
    {
        services.AddScoped<Application.Interfaces.AutoMapper.IMapper, AutoMapper.Mapper>();
    }
}
