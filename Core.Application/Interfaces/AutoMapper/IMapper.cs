using System;

namespace Core.Application.Interfaces.AutoMapper;

public interface IMapper
{
    TDestination Map<TDestination, TSource>(TSource source, string? ignor = null); 
    IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignor = null);
    TDestination Map<TDestination>(object source, string? ignor = null); 
    IList<TDestination> Map<TDestination>(IList<object> source, string? ignor = null);
}
