using System;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.ObjectPool;

namespace Core.Application.Beheviors;

public class FluentValidationBehevior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly IEnumerable<IValidator<TRequest>> validators;

    public FluentValidationBehevior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .GroupBy(g=> g.ErrorMessage)
            .Select(f => f.First())
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
            throw new ValidationException(failures);
            
        return next();
    }
}
