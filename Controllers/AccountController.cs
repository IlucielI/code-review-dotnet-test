using Microsoft.AspNetCore.Mvc;

namespace DotnetBenchmark.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    [HttpGet("redirect")]
    public IActionResult RedirectToExternal([FromQuery] string returnUrl)
    {
        // Vulnerability: Open redirect using unvalidated Redirect() instead of LocalRedirect()
        return Redirect(returnUrl);
    }
}
