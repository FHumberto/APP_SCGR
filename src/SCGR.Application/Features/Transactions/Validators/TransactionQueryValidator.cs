using FluentValidation;
using SCGR.Application.Common.Params;
using SCGR.Application.Common.Validators;
using SCGR.Application.Features.Transactions.Requests;

namespace SCGR.Application.Features.Transactions.Validators;

public class TransactionQueryValidator : AbstractValidator<GetTransactionDateRangeQueryDto>
{
    public TransactionQueryValidator()
    {
        RuleFor(x => x.DateRange)
            .SetValidator(new DateParamsValidator<DateParams>());

        RuleFor(x => x.Pagination)
            .SetValidator(new PaginationParamsValidator<PaginationParams>());
    }
}
