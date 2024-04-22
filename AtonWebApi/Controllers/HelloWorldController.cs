using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtonWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World" + User.Claims.First(x => x.Type == ClaimTypes.Role).Value);
    }
}