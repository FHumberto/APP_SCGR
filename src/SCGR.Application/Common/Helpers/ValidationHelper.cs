using FluentValidation.Results;
using SCGR.Domain.Abstractions.Errors;

namespace SCGR.Application.Common.Helpers;

public static class ValidationHelper
{
    public static Error ToValidationError(ValidationResult validationResult)
    {
        Dictionary<string, string[]> errorDictionary = validationResult.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(e => e.ErrorMessage).ToArray()
            );

        return Error.Validation("Validation.Failed", "Ocorreram erros de validação.", errorDictionary);
    }
}
