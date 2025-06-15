using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace SCGR.API.Controllers.v1;

[ApiVersion(1)]
public class ValuesController : BaseController
{
    [HttpGet]
    public IActionResult Teste() => Ok("Estou funcionando!");

    [HttpGet("exception")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult CasoException() => throw new NotImplementedException();
}
