using Microsoft.AspNetCore.Mvc;

namespace SCGR.API.Controllers;

[Route("api/[controller]")]
public class ValuesController : BaseController
{
    [HttpGet]
    public IActionResult Teste() => Ok("Estou funcionando!");

    [HttpGet("exception")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult CasoException() => throw new NotImplementedException();
}
