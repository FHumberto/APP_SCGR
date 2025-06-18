using SCGR.Domain.Entities.Categories;

namespace SCGR.Domain.UnitTests.Categories;

/// <summary>
/// Contém testes unitários da entidade <see cref="Category"/> para validar as regras de atualização.
/// </summary>
/// <remarks>
/// Testes realizados:
/// <list type="bullet">
///   <item><description>Atualização de nome com valor válido (esperado: sucesso).</description></item>
///   <item><description>Atualização de nome com valor nulo (esperado: exceção de validação).</description></item>
///   <item><description>Atualização de nome com valor vazio (esperado: exceção de validação).</description></item>
///   <item><description>Atualização de nome muito curto (esperado: exceção de validação).</description></item>
///   <item><description>Atualização de nome muito longo (esperado: exceção de validação).</description></item>
/// </list>
/// </remarks>
public sealed class CategoryUpdateTests
{
    /// <summary>
    /// Testa a atualização de uma categoria com nome válido.
    /// </summary>
    /// <remarks>
    /// Espera-se que o nome seja atualizado corretamente e nenhuma exceção seja lançada.
    /// </remarks>
    [Fact]
    public void UpdateCategory_WithValidName_ShouldUpdateSuccessfully()
    {
        // Arrange
        Category? category = new(1, "Categoria Original");
        string newName = "Nova Categoria";

        // Act
        category.Update(newName);

        // Assert
        category.Name.ShouldBe(newName);
    }

    /// <summary>
    /// Testa a atualização de uma categoria com nome nulo.
    /// </summary>
    /// <remarks>
    /// Espera-se que uma <see cref="DomainValidationException"/> seja lançada com a descrição de nome nulo ou vazio.
    /// </remarks>
    [Fact]
    public void UpdateCategory_WithNullName_ShouldThrowDomainValidationException()
    {
        // Arrange
        Category? category = new(1, "Categoria Original");

        // Act
        Action action = () => category.Update(null!);

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(CategoryErrors.CategoryNameIsNullOrEmpty.Description);
    }

    /// <summary>
    /// Testa a atualização de uma categoria com nome vazio.
    /// </summary>
    /// <remarks>
    /// Espera-se que uma <see cref="DomainValidationException"/> seja lançada com a descrição de nome nulo ou vazio.
    /// </remarks>
    [Fact]
    public void UpdateCategory_WithEmptyName_ShouldThrowDomainValidationException()
    {
        // Arrange
        Category category = new(1, "Categoria Original");

        // Act
        Action action = () => category.Update("");

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(CategoryErrors.CategoryNameIsNullOrEmpty.Description);
    }

    /// <summary>
    /// Testa a atualização de uma categoria com nome muito curto.
    /// </summary>
    /// <remarks>
    /// Espera-se que uma <see cref="DomainValidationException"/> seja lançada com a descrição de nome curto demais.
    /// </remarks>
    [Fact]
    public void UpdateCategory_WithShortName_ShouldThrowDomainValidationException()
    {
        // Arrange
        Category category = new(1, "Categoria Original");

        // Act
        Action action = () => category.Update("Ca");

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(CategoryErrors.CategoryNameTooShort.Description);
    }

    /// <summary>
    /// Testa a atualização de uma categoria com nome muito longo.
    /// </summary>
    /// <remarks>
    /// Espera-se que uma <see cref="DomainValidationException"/> seja lançada com a descrição de nome longo demais.
    /// </remarks>
    [Fact]
    public void UpdateCategory_WithTooLongName_ShouldThrowDomainValidationException()
    {
        // Arrange
        Category category = new(1, "Categoria Original");
        string longName = new('A', 101);

        // Act
        Action action = () => category.Update(longName);

        // Assert
        DomainValidationException exception = action.ShouldThrow<DomainValidationException>();
        exception.Message.ShouldBe(CategoryErrors.CategoryNameTooLong.Description);
    }
}
