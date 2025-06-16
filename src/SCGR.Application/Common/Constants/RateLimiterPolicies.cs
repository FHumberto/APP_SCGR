namespace SCGR.Application.Common.Constants;

/// <summary>
/// Contém constantes relacionadas às políticas de rate limiting.
/// </summary>
public static class RateLimiterPolicies
{
    /// <summary>
    /// Limita requisições com base em uma janela de tempo fixa.
    /// </summary>
    public const string Fixed = "Fixed";
}
