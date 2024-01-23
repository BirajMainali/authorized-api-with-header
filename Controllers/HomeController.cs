using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authorized_api_header.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return Ok("Hello World!");
    }

    [Authorize]
    [HttpGet("secret")]
    public IActionResult Secret()
    {
        return Ok("You found the secret!");
    }
}