using Microsoft.AspNetCore.Mvc;
using SCGR.Domain.Abstractions.Errors;

namespace SCGR.API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Cria uma resposta de erro padronizada do tipo <see cref="ProblemDetails"/> com base no tipo de erro fornecido.
    /// </summary>
    /// <param name="error">
    /// Objeto do tipo <see cref="Error"/> que contém os detalhes do erro, como código, descrição e tipo.
    /// </param>
    /// <remarks>
    /// A resposta segue o formato do padrão de erro definido por <see href="https://datatracker.ietf.org/doc/html/rfc7807">RFC 7807</see>.
    /// </remarks>
    /// <returns>
    /// Um <see cref="IActionResult"/> contendo uma resposta HTTP com status apropriado e detalhes do problema, conforme o padrão RFC 7807.
    /// </returns>
    protected IActionResult Problem(Error error)
    {
        int statusCode = error.ErrorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.AccessUnAuthorized => StatusCodes.Status401Unauthorized,
            ErrorType.AccessForbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        //? adiciona um dicionário de erros para validação
        if (error.ValidationDetails is not null && error.ErrorType == ErrorType.Validation)
        {
            ProblemDetails problemDetails = new()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
                Title = error.Description,
                Detail = error.Code,
                Status = statusCode
            };

            problemDetails.Extensions["errors"] = error.ValidationDetails;

            return StatusCode(statusCode, problemDetails);
        }

        return Problem(statusCode: statusCode, detail: error.Code, title: error.Description);
    }
}
