using SCGR.Domain.Abstractions.Errors;

namespace SCGR.Domain.Entities.Transactions;

public static class TransactionErrors
{
    public static readonly Error TransactionNotFound
        = Error.NotFound("Error.TransactionNotFound", "Transação não encontrada.");

    public static readonly Error DescriptionIsNullOrEmpty
        = Error.Validation("Error.DescriptionIsNullOrEmpty", "A descrição da transação está vazia ou nula.");

    public static readonly Error DescriptionTooLong
        = Error.Validation("Error.DescriptionTooLong", "A descrição da transação é muito longa.");

    public static readonly Error AmountMustBeGreaterThanZero
        = Error.Validation("Error.AmountMustBeGreaterThanZero", "O valor da transação deve ser maior que zero.");

    public static readonly Error InvalidTransactionDate
        = Error.Validation("Error.InvalidTransactionDate", "A data da transação é inválida.");

    public static readonly Error InvalidCategoryId
        = Error.Validation("Error.InvalidCategoryId", "A categoria informada para a transação é inválida.");
}
