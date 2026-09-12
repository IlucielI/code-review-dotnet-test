using Microsoft.AspNetCore.Cors.Infrastructure;

namespace DotnetBenchmark.Services;

public class CorsSetup
{
    // Insecure CORS: AllowAnyOrigin with AllowCredentials
    public static void Configure(CorsPolicyBuilder builder)
    {
        builder.AllowAnyOrigin().AllowCredentials();
    }
}
