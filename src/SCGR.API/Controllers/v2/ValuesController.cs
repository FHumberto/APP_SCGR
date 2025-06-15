using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace SCGR.API.Controllers.v2;

[ApiVersion(2)]
public class ValuesController : BaseController
{
    [HttpGet]
    public IActionResult Teste() => Ok("Estou funcionando! na V2");

    [HttpGet("exception")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult CasoException() => throw new NotImplementedException();
}
