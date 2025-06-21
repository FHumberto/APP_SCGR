using SCGR.Application.Common.Params;

namespace SCGR.Application.Features.Transactions.Requests;

public class GetTransactionDateRangeQueryDto
{
    public required PaginationParams Pagination { get; set; }
    public required DateParams DateRange { get; set; }
}