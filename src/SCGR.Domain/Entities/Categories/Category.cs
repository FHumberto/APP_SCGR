using SCGR.Domain.Abstractions.Errors;
using SCGR.Domain.Abstractions.Types;
using SCGR.Domain.Entities.Transactions;
using SCGR.Domain.Exceptions;

namespace SCGR.Domain.Entities.Categories;

public sealed class Category : Entity
{
    public string Name { get; private set; }

    #region [ ORM ]

    public ICollection<Transaction>? Transactions { get; set; }

    #endregion

    public Category(string name)
    {
        Name = name;
        IsValid();
    }

    public Category(int id, string name)
    {
        DomainValidationException.When(
            hasError: id < 0,
            error: EntityErrors.EntityInvalid.Description);

        Id = id;
        Name = name;

        IsValid();
    }

    public void Update(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;

        IsValid();
    }

    private void IsValid()
    {
        DomainValidationException.When(
            hasError: string.IsNullOrWhiteSpace(Name),
            error: CategoryErrors.CategoryNameIsNullOrEmpty.Description);

        DomainValidationException.When(
            hasError: Name.Length < 3,
            error: CategoryErrors.CategoryNameTooShort.Description);

        DomainValidationException.When(
            hasError: Name.Length > 100,
            error: CategoryErrors.CategoryNameTooLong.Description);
    }
}
