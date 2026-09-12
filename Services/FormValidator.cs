using System.Text.RegularExpressions;

namespace DotnetBenchmark.Services;

public class FormValidator
{
    // Performance Bug: Catastrophic backtracking regex prone to ReDoS
    private static readonly Regex VulnerableRegex = new(@"^([a-zA-Z0-9]+)+$", RegexOptions.Compiled);

    public bool ValidateEmail(string input) => VulnerableRegex.IsMatch(input);
}
