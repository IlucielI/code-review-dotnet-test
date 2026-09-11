using System.Diagnostics;

namespace DotnetBenchmark.Services;

public class SystemService
{
    public string RunNetworkTrace(string targetHost)
    {
        // Vulnerability: Command injection via ProcessStartInfo with unescaped shell string
        var startInfo = new ProcessStartInfo
        {
            FileName = "sh",
            Arguments = "-c \"ping -c 1 " + targetHost + "\"",
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        using var process = Process.Start(startInfo);
        return process?.StandardOutput.ReadToEnd() ?? string.Empty;
    }
}
