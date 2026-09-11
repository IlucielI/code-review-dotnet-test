using Microsoft.EntityFrameworkCore;
using DotnetBenchmark.Models;

namespace DotnetBenchmark.Repositories;

public class UserRepository
{
    private readonly DbContext _context;

    public UserRepository(DbContext context)
    {
        _context = context;
    }

    public List<User> FindUsersByName(string username)
    {
        // Vulnerability: SQL Injection via EF Core FromSqlRaw string interpolation
        return _context.Set<User>()
            .FromSqlRaw($"SELECT * FROM Users WHERE Username = '{username}'")
            .ToList();
    }
}
