using System;
using AutoMapper;
using AutoMapper.Internal;

namespace Core.Mapper.AutoMapper;

public class Mapper : Application.Interfaces.AutoMapper.IMapper
{
    public static List<TypePair> typePairs = new List<TypePair>();

    private IMapper mapperContainer; 

    public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
    {
        Config<TDestination, TSource>(ignore: ignore);
        return mapperContainer.Map<TSource, TDestination>(source);
    }

    public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
    {
        Config<TDestination, TSource>(ignore: ignore);
        return mapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
    }

    public TDestination Map<TDestination>(object source, string? ignore = null)
    {
        Config<TDestination, object>(ignore: ignore);
        return mapperContainer.Map<object, TDestination>(source);
    }

    public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
    {
        Config<TDestination, object>(ignore: ignore);
        return mapperContainer.Map<IList<object>, IList<TDestination>>(source);
    }
    
    private void Config<TDestination, TSource>(int dept = 5, string? ignore = null)
    {
        var TypePair = new TypePair(typeof(TDestination), typeof(TSource));

        if (typePairs.Any(x=> x.DestinationType == TypePair.DestinationType && x.SourceType == TypePair.SourceType)) return;

        typePairs.Add(TypePair);

        var config = new MapperConfiguration(cfg =>
        {
            foreach (var item in typePairs)
            {
                if (ignore is not null)
                    cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(dept).ForMember(ignore, opt => opt.Ignore()).ReverseMap();
                else 
                    cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(dept).ReverseMap();
            }
        });

        mapperContainer = config.CreateMapper();
    }
}
