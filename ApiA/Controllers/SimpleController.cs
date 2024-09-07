using Microsoft.AspNetCore.Mvc;

namespace ApiA.Controllers;

[ApiController]
[Route("simple")]
public class SimpleController : ControllerBase
{
    [HttpGet("hello")]
    public IActionResult GetSimpleMessage()
    {
        return Ok("Hello from API A!");
    }
}