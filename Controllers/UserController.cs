using Microsoft.AspNetCore.Mvc;
using DotnetBenchmark.Models;

namespace DotnetBenchmark.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("update")]
    public IActionResult UpdateUserProfile([FromBody] User user)
    {
        // Vulnerability: Overposting / Mass Assignment - client can directly overwrite Role and IsAdmin
        return Ok(new { status = "updated", user.Username, user.Role, user.IsAdmin });
    }
}
