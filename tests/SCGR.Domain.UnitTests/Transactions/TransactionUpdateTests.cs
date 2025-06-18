using SCGR.Domain.Entities.Transactions;

namespace SCGR.Domain.UnitTests.Transactions;

/// <summary>
/// Contém testes unitários da entidade <see cref="Transaction"/> para validar regras de atualização.
/// </summary>
/// <remarks>
/// Testes realizados:
/// <list type="bullet">
///   <item><description>Atualização com dados válidos (esperado: sucesso).</description></item>
///   <item><description>Atualização com descrição vazia (esperado: exceção de validação).</description></item>
///   <item><description>Atualização com descrição muito longa (esperado: exceção de validação).</description></item>
///   <item><description>Atualização com valor inválido (esperado: exceção de validação).</description></item>
///   <item><description>Atualização com data inválida (esperado: exceção de validação).</description></item>
///   <item><description>Atualização com ID de categoria inválido (esperado: exceção de validação).</description></item>
/// </list>
/// </remarks>
public sealed class TransactionUpdateTests
{
    /// <summary>
    /// Testa a atualização de uma transação com dados válidos.
    /// </summary>
    [Fact]
    public void UpdateTransaction_WithValidData_ShouldUpdateSuccessfully()
    {
        // Arrange
        DateOnly date = new(2024, 10, 10);
        Transaction transaction = new(TransactionType.Income, "Original", 100, date, 1);
        DateOnly updatedDate = new(2024, 12, 25);

        // Act
        transaction.Update("Atualizada", 500, updatedDate, 2);

        // Assert
        transaction.Description.ShouldBe("Atualizada");
        transaction.Amount.ShouldBe(500);
        transaction.TransactionDate.ShouldBe(updatedDate);
        transaction.CategoryId.ShouldBe(2);
    }

    /// <summary>
    /// Testa a atualização com descrição vazia.
    /// </summary>
    [Fact]
    public void UpdateTransaction_WithEmptyDescription_ShouldThrowDomainValidationException()
    {
        // Arrange
        Transaction transaction = new(TransactionType.Expense, "Compra", 200, new DateOnly(2024, 1, 1), 1);

        // Act
        Action action = () => transaction.Update("", 200, new DateOnly(2024, 1, 1), 1);

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.DescriptionIsNullOrEmpty.Description);
    }

    /// <summary>
    /// Testa a atualização com descrição muito longa.
    /// </summary>
    [Fact]
    public void UpdateTransaction_WithTooLongDescription_ShouldThrowDomainValidationException()
    {
        // Arrange
        Transaction transaction = new(TransactionType.Income, "Renda", 100, new DateOnly(2024, 1, 1), 1);
        string longDescription = new('x', 201);

        // Act
        Action action = () => transaction.Update(longDescription, 100, new DateOnly(2024, 1, 1), 1);

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.DescriptionTooLong.Description);
    }

    /// <summary>
    /// Testa a atualização com valor igual a zero.
    /// </summary>
    [Fact]
    public void UpdateTransaction_WithZeroAmount_ShouldThrowDomainValidationException()
    {
        // Arrange
        Transaction transaction = new(TransactionType.Expense, "Compra", 100, new DateOnly(2024, 1, 1), 1);

        // Act
        Action action = () => transaction.Update("Nova Compra", 0, new DateOnly(2024, 1, 1), 1);

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.AmountMustBeGreaterThanZero.Description);
    }

    /// <summary>
    /// Testa a atualização com data inválida (default).
    /// </summary>
    [Fact]
    public void UpdateTransaction_WithInvalidDate_ShouldThrowDomainValidationException()
    {
        // Arrange
        Transaction transaction = new(TransactionType.Expense, "Compra", 100, new DateOnly(2024, 1, 1), 1);

        // Act
        Action action = () => transaction.Update("Nova Compra", 100, default, 1);

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.InvalidTransactionDate.Description);
    }

    /// <summary>
    /// Testa a atualização com categoria inválida (ID menor ou igual a 0).
    /// </summary>
    [Fact]
    public void UpdateTransaction_WithInvalidCategoryId_ShouldThrowDomainValidationException()
    {
        // Arrange
        Transaction transaction = new(TransactionType.Expense, "Compra", 100, new DateOnly(2024, 1, 1), 1);

        // Act
        Action action = () => transaction.Update("Nova Compra", 100, new DateOnly(2024, 1, 1), 0);

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.InvalidCategoryId.Description);
    }
}
