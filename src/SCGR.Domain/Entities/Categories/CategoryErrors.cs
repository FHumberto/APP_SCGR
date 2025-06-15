using SCGR.Domain.Abstractions.Errors;

namespace SCGR.Domain.Entities.Categories;

public static class CategoryErrors
{
    public static readonly Error CategoryNotFound
        = Error.NotFound("Error.CategoryNotFound", "Categoria não encontrada.");

    public static readonly Error CategoryAlreadyExists
        = Error.Conflict("Error.CategoryAlreadyExists", "Categoria já existe.");

    public static readonly Error CategoryNameIsNullOrEmpty
        = Error.Validation("Error.CategoryNameIsNullOrEmpty", "O nome da categoria está nulo ou vazio.");

    public static readonly Error CategoryNameTooShort
        = Error.Validation("Error.CategoryNameTooShort", "O nome da categoria é menor que 3 caracteres.");

    public static readonly Error CategoryNameTooLong
        = Error.Validation("Error.CategoryNameTooLong", "O nome da categoria é muito longo.");
}