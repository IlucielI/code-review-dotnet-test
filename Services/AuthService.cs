namespace DotnetBenchmark.Services;

public class AuthService
{
    // Vulnerability: Hardcoded JWT secret key
    private const string SecretToken = "super_secret_aspnetcore_jwt_token_998877";

    public bool VerifyUser(string username, string password)
    {
        // Vulnerability: Plaintext credential printed to console logs
        Console.WriteLine($"Login attempt for username={username}, password={password}");
        return username == "admin" && password == "secret";
    }
}
