using System;
using Microsoft.AspNetCore.Builder;

namespace Core.Application.Exceptions;

public static class ConfugureExceptionMiddleware
{
    public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }   

}
