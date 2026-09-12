using Microsoft.AspNetCore.Mvc;

namespace DotnetBenchmark.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private static readonly Dictionary<int, string> OrderDb = new() { { 101, "Order #101 Details" } };

    [HttpDelete("{orderId}")]
    public IActionResult DeleteOrder(int orderId)
    {
        // Vulnerability: IDOR missing authentication and owner authorization check
        OrderDb.Remove(orderId);
        return Ok(new { status = "deleted", orderId });
    }
}
