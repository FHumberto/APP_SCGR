using SCGR.Domain.Entities.Categories;

namespace SCGR.Domain.UnitTests.Categories;

/// <summary>
/// Contém testes unitários da entidade <see cref="Category"/> para validar as regras de negócio.
/// </summary>
/// <remarks>
/// Testes realizados:
/// <list type="bullet">
///   <item><description>Criação de categoria com ID e nome válidos (esperado: sucesso).</description></item>
///   <item><description>Criação de categoria com ID negativo (esperado: exceção de validação).</description></item>
///   <item><description>Criação de categoria com nome muito curto (esperado: exceção de validação).</description></item>
///   <item><description>Criação de categoria com nome vazio (esperado: exceção de validação).</description></item>
///   <item><description>Criação de categoria com nome nulo (esperado: exceção de validação).</description></item>
/// </list>
/// </remarks>
public sealed class CategoryCreationTests
{
    /// <summary>
    /// Testa a criação de uma categoria com ID e nome válidos.
    /// </summary>
    /// <remarks>
    /// Espera-se que nenhum erro de domínio seja lançado.
    /// </remarks>
    [Fact]
    public void CreateCategory_WithValidParameters_ShouldNotThrowException()
    {
        // Arrange & Act
        Action action = () => _ = new Category(1, "Nome da Categoria");

        // Assert
        action.ShouldNotThrow();
    }

    /// <summary>
    /// Testa a criação de uma categoria com ID negativo.
    /// </summary>
    /// <remarks>
    /// Espera-se que uma <see cref="DomainValidationException"/> seja lançada com a mensagem "Invalid Id value".
    /// </remarks>
    [Fact]
    public void CreateCategory_WithNegativeId_ShouldThrowDomainValidationException()
    {
        // Arrange & Act
        Action action = () => _ = new Category(-1, "Nome da Categoria");

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(EntityErrors.EntityInvalid.Description);
    }

    /// <summary>
    /// Testa a criação de uma categoria com nome muito curto.
    /// </summary>
    /// <remarks>
    /// Deve lançar <see cref="DomainValidationException"/> com a descrição definida em <see cref="CategoryErrors.CategoryNameTooShort"/>.
    /// </remarks>
    [Fact]
    public void CreateCategory_WithShortName_ShouldThrowDomainValidationException()
    {
        // Arrange & Act
        Action action = () => _ = new Category(1, "Ca");

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(CategoryErrors.CategoryNameTooShort.Description);
    }

    /// <summary>
    /// Testa a criação de uma categoria com nome vazio.
    /// </summary>
    /// <remarks>
    /// Deve lançar <see cref="DomainValidationException"/> com a descrição definida em <see cref="CategoryErrors.CategoryNameIsNullOrEmpty"/>.
    /// </remarks>
    [Fact]
    public void CreateCategory_WithEmptyName_ShouldThrowDomainValidationException()
    {
        // Arrange & Act
        Action action = () => _ = new Category(1, "");

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(CategoryErrors.CategoryNameIsNullOrEmpty.Description);
    }

    /// <summary>
    /// Testa a criação de uma categoria com nome nulo.
    /// </summary>
    /// <remarks>
    /// Deve lançar <see cref="DomainValidationException"/> ao tentar criar a categoria com nome <c>null</c>.
    /// </remarks>
    [Fact]
    public void CreateCategory_WithNullName_ShouldThrowDomainValidationException()
    {
        // Arrange & Act
        Action action = () => _ = new Category(1, null!);

        // Assert
        _ = action.ShouldThrow<DomainValidationException>();
    }
}
