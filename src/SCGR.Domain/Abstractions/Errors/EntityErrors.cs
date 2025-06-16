namespace SCGR.Domain.Abstractions.Errors;

public static class EntityErrors
{
    public static readonly Error EntityInvalid =
        Error.Validation("Error.EntityInvalid", "O ID informado é inválido.");
}
