using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SCGR.Application.Common.Wrappers;
using SCGR.Domain.Abstractions.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace SCGR.API.Controllers.v1;

[ApiVersion("1")]
public class ValuesController : BaseController
{
    public record TesteDto(string Nome, string Description);

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(Summary = "Teste", Description = "Descricao de Teste")]
    public IActionResult Teste()
    {
        Result<TesteDto> result = new TesteDto("Teste", "Descrição de teste.");

        return result.Match(onSuccess: Ok, onFailure: Problem);
    }

    [HttpGet("erro")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult CasoErro()
    {
        Error error = Error.Failure("ERRO", "Erro de Teste");
        Result<TesteDto> result = Result<TesteDto>.Failure(error);

        return result.Match(onSuccess: Ok, onFailure: Problem);
    }

    [HttpGet("exception")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult CasoException() => throw new NotImplementedException();

}
