using FluentValidation;
using SCGR.Application.Contracts.Persistence;
using SCGR.Application.Features.Transactions.Requests;

namespace SCGR.Application.Features.Transactions.Validators;

public class TransactionCommandValidator : AbstractValidator<TransactionCommandDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public TransactionCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.TransactionType)
            .IsInEnum()
            .WithMessage("O tipo de transação é inválido.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("A descrição é obrigatória.")
            .MaximumLength(500)
            .WithMessage("A descrição não deve exceder 500 caracteres.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("O valor deve ser maior que zero.");

        RuleFor(x => x.TransactionDate)
            .NotEmpty()
            .WithMessage("A data da transação é obrigatória.");

        RuleFor(x => x.CategoryId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O ID da categoria deve ser maior ou igual a zero.")
            .MustAsync(CategoryExists)
            .WithMessage("A categoria informada não existe.");
    }

    private async Task<bool> CategoryExists(int categoryId, CancellationToken cancellationToken)
        => await _categoryRepository.ExistsAsync(categoryId);
}
