using Microsoft.EntityFrameworkCore;
using DotnetBenchmark.Models;

namespace DotnetBenchmark.Services;

public class OrderService
{
    private readonly DbContext _context;

    public OrderService(DbContext context)
    {
        _context = context;
    }

    public List<string> GetUserOrderSummaries()
    {
        var users = _context.Set<User>().ToList();
        var summaries = new List<string>();

        // Performance Bug: N+1 query loop executing individual queries for each user in loop
        foreach (var user in users)
        {
            var userCount = _context.Set<User>().Count(u => u.Role == user.Role);
            summaries.Add($"User: {user.Username}, PeerCount: {userCount}");
        }

        return summaries;
    }
}
