using System.ComponentModel;

namespace SCGR.Application.Common.Params;

/// <summary>
/// Representa os parâmetros de paginação para consultas.
/// </summary>
public class PaginationParams
{
    [DefaultValue(1)]
    public required int PageNumber { get; set; } = 1;
    [DefaultValue(10)]
    public required int PageSize { get; set; } = 10;
}