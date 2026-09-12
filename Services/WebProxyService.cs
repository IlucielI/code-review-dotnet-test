namespace DotnetBenchmark.Services;

public class WebProxyService
{
    public async Task<string> ForwardRequestAsync(string destinationUrl)
    {
        // Vulnerability: SSRF via unvalidated user URL request
        using var client = new HttpClient();
        return await client.GetStringAsync(destinationUrl);
    }
}
