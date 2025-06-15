using Microsoft.AspNetCore.Mvc;

namespace SCGR.API.Controllers;

[Route("api/[controller]")]
public class ValuesController : BaseController
{
    [HttpGet]
    public IActionResult Teste() => Ok("Estou funcionando!");
}
