using Microsoft.AspNetCore.Mvc;

namespace SCGR.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult Teste() => Ok("Estou funcionando!");
}
