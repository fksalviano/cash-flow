using FluentValidation.Results;

namespace Domain.Validation;

public static class ResultExtensions
{

    public static Result ToResult(this ValidationResult validation) => new 
    (
        validation.IsValid, 
        string.Join(", ", validation.Errors.Select(error => error.ErrorMessage))
    );
}
