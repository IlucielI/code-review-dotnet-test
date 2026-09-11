using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetBenchmark.Models;

namespace DotnetBenchmark.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SafeGuardController : ControllerBase
{
    private readonly DbContext _context;

    public SafeGuardController(DbContext context)
    {
        _context = context;
    }

    [HttpGet("safe-user")]
    public async Task<IActionResult> GetUserSafe([FromQuery] string username)
    {
        // Guard: EF Core parameterized query with FormattableString - NOT SQL injection
        var user = await _context.Set<User>()
            .FromSqlInterpolated($"SELECT * FROM Users WHERE Username = {username}")
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return Ok(user);
    }

    [HttpGet("safe-redirect")]
    public IActionResult SafeRedirect([FromQuery] string returnUrl)
    {
        // Guard: LocalRedirect validates relative/local URL - NOT open redirect
        if (Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }
        return Redirect("/home");
    }

    [HttpPost("safe-profile")]
    public IActionResult SafeProfileUpdate([Bind("Username")] User user)
    {
        // Guard: [Bind] attribute restricts overposting of Role/IsAdmin - NOT mass assignment
        return Ok(new { status = "safe_updated", user.Username });
    }
}
