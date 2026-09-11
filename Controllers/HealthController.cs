using Microsoft.AspNetCore.Mvc;

namespace DotnetBenchmark.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet("/healthz")]
    public IActionResult Health()
    {
        return Ok(new { status = "ok", service = "code-review-dotnet-test", framework = "asp.net core" });
    }
}
