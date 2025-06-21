using SCGR.Domain.Abstractions.Errors;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Categories;
using SCGR.Domain.Exceptions;

namespace SCGR.Domain.Entities.Transactions;

public sealed class Transaction : Entity
{
    public TransactionType TransactionType { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public DateOnly TransactionDate { get; private set; }

    #region [ ORM ]

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    #endregion

    public Transaction(TransactionType transactionType, string description, decimal amount, DateOnly transactionDate, int categoryId)
    {
        TransactionType = transactionType;
        Description = description;
        Amount = amount;
        TransactionDate = transactionDate;
        CategoryId = categoryId;

        IsValid();
    }

    public Transaction(int id, TransactionType transactionType, string description, decimal amount, DateOnly transactionDate, int categoryId)
    {
        DomainValidationException.When(
            hasError: id < 0,
            error: EntityErrors.EntityInvalid.Description);

        Id = id;
        TransactionType = transactionType;
        Description = description;
        Amount = amount;
        TransactionDate = transactionDate;
        CategoryId = categoryId;

        IsValid();
    }

    public void Update(TransactionType transactionType, string description, decimal amount, DateOnly transactionDate, int categoryId)
    {
        TransactionType = transactionType;
        Description = description;
        Amount = amount;
        TransactionDate = transactionDate;
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;

        IsValid();
    }

    private void IsValid()
    {
        DomainValidationException.When(
            hasError: string.IsNullOrWhiteSpace(Description),
            error: TransactionErrors.DescriptionIsNullOrEmpty.Description);

        DomainValidationException.When(
            hasError: Description.Length > 200,
            error: TransactionErrors.DescriptionTooLong.Description);

        DomainValidationException.When(
            hasError: Amount <= 0,
            error: TransactionErrors.AmountMustBeGreaterThanZero.Description);

        DomainValidationException.When(
            hasError: TransactionDate == default,
            error: TransactionErrors.InvalidTransactionDate.Description);

        DomainValidationException.When(
            hasError: CategoryId <= 0,
            error: TransactionErrors.InvalidCategoryId.Description);
    }
}
