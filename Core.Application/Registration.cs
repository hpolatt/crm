using System.Globalization;
using System.Reflection;
using Core.Application.Base;
using Core.Application.Beheviors;
using Core.Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class Registration
{

    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddTransient<ExceptionMiddleware>();
        services.AddRulesFromAssembly(assembly, typeof(BaseRule));
        services.AddValidatorsFromAssembly(assembly);
        ValidatorOptions.Global.LanguageManager.Culture= new CultureInfo("en-US");

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
        


    }


    // Add all rules from assembly to DI
    private static IServiceCollection AddRulesFromAssembly(this IServiceCollection services, Assembly assembly, Type baseType)
    {
        var rules = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(baseType) && t != baseType)
            .ToList();

        foreach (var rule in rules)
            services.AddTransient(rule);

        return services;
    }


}
