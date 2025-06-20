using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCGR.Application.Common.Params;

/// <summary>
/// Representa os parâmetros de intervalo de datas para consultas.
/// </summary>
public class DateParams
{
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DefaultValue(typeof(DateOnly), $"2025-01-01")]
    public required DateOnly StartDate { get; set; } = DateOnly.MinValue;

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DefaultValue(typeof(DateOnly), "9999-12-31")]
    public required DateOnly EndDate { get; set; } = DateOnly.MaxValue;
}