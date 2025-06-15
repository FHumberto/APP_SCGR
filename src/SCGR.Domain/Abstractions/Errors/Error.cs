namespace SCGR.Domain.Abstractions.Errors;

public sealed class Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType ErrorType { get; }
    public IDictionary<string, string[]>? ValidationDetails { get; }

    private Error(string code, string description, ErrorType errorType, IDictionary<string, string[]>? validationDetails = null)
    {
        Code = code;
        Description = description;
        ErrorType = errorType;
        ValidationDetails = validationDetails;
    }

    public static Error Failure(string code, string description)
        => new(code, description, ErrorType.Failure);

    public static Error Validation(string code, string description, IDictionary<string, string[]>? validationDetails = null)
        => new(code, description, ErrorType.Validation, validationDetails);

    public static Error AccessUnAuthorized(string code, string description)
        => new(code, description, ErrorType.AccessUnAuthorized);

    public static Error AccessForbidden(string code, string description)
        => new(code, description, ErrorType.AccessForbidden);

    public static Error NotFound(string code, string description)
        => new(code, description, ErrorType.NotFound);

    public static Error Conflict(string code, string description)
        => new(code, description, ErrorType.Conflict);
}