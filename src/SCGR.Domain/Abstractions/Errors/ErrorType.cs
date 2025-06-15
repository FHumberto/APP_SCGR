namespace SCGR.Domain.Abstractions.Errors;

public enum ErrorType
{
    Failure = 500,
    Validation = 400,
    AccessUnAuthorized = 401,
    AccessForbidden = 403,
    NotFound = 404,
    Conflict = 409
}
