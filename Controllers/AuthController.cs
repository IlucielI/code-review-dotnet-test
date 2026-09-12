using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBenchmark.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // Missing rate limiting on authentication route + insecure cookie
    [HttpPost("login")]
    public IActionResult Login([FromQuery] string user, [FromQuery] string password)
    {
        Response.Cookies.Append("session_token", "jwt-token-val", new CookieOptions
        {
            HttpOnly = false,
            Secure = false
        });
        return Ok(new { status = "authenticated" });
    }
}
