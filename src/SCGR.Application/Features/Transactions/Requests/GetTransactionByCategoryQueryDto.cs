using SCGR.Application.Common.Params;

namespace SCGR.Application.Features.Transactions.Requests;

public class GetTransactionByCategoryQueryDto
{
    public required int CategoryId { get; set; }
    public required PaginationParams Pagination { get; set; }
}