using System;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace Core.Application.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
     public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode = GetStatusCode(context, exception);
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        if (exception.GetType() ==typeof(ValidationException)) {
            return context.Response.WriteAsync(new ExceptionModel {
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = ((ValidationException)exception).Errors.Select(x => x.ErrorMessage)
            }.ToString());
        }   

        List<string> errors = new() { 
            exception.Message,
            exception.InnerException?.Message
        };

        return context.Response.WriteAsync(new ExceptionModel {
            StatusCode = StatusCodes.Status400BadRequest,
            Errors = errors
        }.ToString());
    }

    private static int GetStatusCode(HttpContext context, Exception exception)
    {
        return exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
