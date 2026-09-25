using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace CargoFlow.BuildingBlocks.Presentation.Extensions;


public static class ErrorOrExtension
{
    public static IResult ToProblem(this List<Error> errors)
    {
        var firstError= errors[0];
        return firstError.Type switch
        {
            ErrorType.Validation => Results.ValidationProblem(
                errors.ToDictionary(
                    x => x.Code,
                    x => new[] { x.Description }
                    )
                ),
            ErrorType.NotFound => Results.NotFound(
                      errors.Select(e => e.Description)),
            ErrorType.Conflict => Results.Conflict(
                      errors.Select(e => e.Description)
                ),
            ErrorType.Unauthorized => Results.Unauthorized(),
            ErrorType.Forbidden => Results.Forbid(),
            _ => Results.Problem()
        };

    }

}
