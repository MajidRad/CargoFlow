using CargoFlow.BuildingBlocks.Application.Abstractions;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace CargoFlow.BuildingBlocks.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest,ErrorOr<TResponse>>
    where TRequest : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<ErrorOr<TResponse>> Handle(TRequest request, RequestHandlerDelegate<ErrorOr<TResponse>> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults =
             await Task.WhenAll(
             _validators.Select(
             validator => validator.ValidateAsync(
             context,
             cancellationToken)));

        var failures = validationResults
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();
        if (failures.Count != 0)
        {
            var errors = failures
               .Select(failure =>
                   Error.Validation(
                       code: failure.PropertyName,
                       description: failure.ErrorMessage))
               .ToList();
            return errors;

        }
        return await next();
    }
}
