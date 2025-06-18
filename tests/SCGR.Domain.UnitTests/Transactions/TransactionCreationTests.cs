using SCGR.Domain.Entities.Transactions;

namespace SCGR.Domain.UnitTests.Transactions;

/// <summary>
/// Contém testes unitários da entidade <see cref="Transaction"/> para validar as regras de negócio.
/// </summary>
/// <remarks>
/// Testes realizados:
/// <list type="bullet">
///   <item><description>Criação de transação válida (esperado: sucesso).</description></item>
///   <item><description>Criação com ID negativo (esperado: exceção).</description></item>
///   <item><description>Criação com descrição vazia (esperado: exceção).</description></item>
///   <item><description>Criação com descrição longa demais (esperado: exceção).</description></item>
///   <item><description>Criação com valor <= 0 (esperado: exceção).</description></item>
///   <item><description>Criação com data inválida (esperado: exceção).</description></item>
///   <item><description>Criação com ID de categoria inválido (esperado: exceção).</description></item>
///   <item><description>Atualização de dados válidos (esperado: sucesso).</description></item>
/// </list>
/// </remarks>
public sealed class TransactionCreationTests
{
    /// <summary>
    /// Testa a criação de uma transação com parâmetros válidos.
    /// </summary>
    [Fact]
    public void CreateTransaction_WithValidParameters_ShouldNotThrow()
    {
        DateOnly date = new(2024, 10, 10);

        Action action = () => _ = new Transaction(TransactionType.Income, "Salário", 1000, date, 1);

        action.ShouldNotThrow();
    }

    /// <summary>
    /// Testa a criação de uma transação com ID negativo.
    /// </summary>
    [Fact]
    public void CreateTransaction_WithNegativeId_ShouldThrowDomainValidationException()
    {
        DateOnly date = new(2024, 10, 10);

        Action action = () => _ = new Transaction(-1, TransactionType.Expense, "Compra", 100, date, 1);

        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(EntityErrors.EntityInvalid.Description);
    }

    /// <summary>
    /// Testa a criação com descrição nula ou vazia.
    /// </summary>
    [Fact]
    public void CreateTransaction_WithEmptyDescription_ShouldThrowDomainValidationException()
    {
        DateOnly date = new(2024, 10, 10);

        Action action = () => _ = new Transaction(TransactionType.Income, "", 500, date, 1);

        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.DescriptionIsNullOrEmpty.Description);
    }

    /// <summary>
    /// Testa a criação com descrição longa demais.
    /// </summary>
    [Fact]
    public void CreateTransaction_WithTooLongDescription_ShouldThrowDomainValidationException()
    {
        DateOnly date = new(2024, 10, 10);
        string longDescription = new('a', 201);

        Action action = () => _ = new Transaction(TransactionType.Income, longDescription, 500, date, 1);

        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.DescriptionTooLong.Description);
    }

    /// <summary>
    /// Testa a criação com valor igual a zero.
    /// </summary>
    [Fact]
    public void CreateTransaction_WithZeroAmount_ShouldThrowDomainValidationException()
    {
        DateOnly date = new(2024, 10, 10);

        Action action = () => _ = new Transaction(TransactionType.Income, "Venda", 0, date, 1);

        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.AmountMustBeGreaterThanZero.Description);
    }

    /// <summary>
    /// Testa a criação com data de transação inválida (valor default).
    /// </summary>
    [Fact]
    public void CreateTransaction_WithInvalidDate_ShouldThrowDomainValidationException()
    {
        Action action = () => _ = new Transaction(TransactionType.Expense, "Compra", 50, default, 1);

        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.InvalidTransactionDate.Description);
    }

    /// <summary>
    /// Testa a criação com ID de categoria inválido.
    /// </summary>
    [Fact]
    public void CreateTransaction_WithInvalidCategoryId_ShouldThrowDomainValidationException()
    {
        DateOnly date = new(2024, 10, 10);

        Action action = () => _ = new Transaction(TransactionType.Income, "Investimento", 100, date, 0);

        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(TransactionErrors.InvalidCategoryId.Description);
    }

    /// <summary>
    /// Testa a atualização dos dados de uma transação com valores válidos.
    /// </summary>
    [Fact]
    public void UpdateTransaction_WithValidData_ShouldUpdateSuccessfully()
    {
        DateOnly date = new(2024, 10, 10);
        Transaction transaction = _ = new Transaction(TransactionType.Income, "Bônus", 150, date, 1);

        DateOnly newDate = new(2024, 12, 1);
        transaction.Update("Bônus Atualizado", 200, newDate, 2);

        transaction.Description.ShouldBe("Bônus Atualizado");
        transaction.Amount.ShouldBe(200);
        transaction.TransactionDate.ShouldBe(newDate);
        transaction.CategoryId.ShouldBe(2);
    }
}
