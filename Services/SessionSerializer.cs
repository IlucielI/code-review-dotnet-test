using System.Runtime.Serialization.Formatters.Binary;

namespace DotnetBenchmark.Services;

public class SessionSerializer
{
    public object DeserializeSession(byte[] sessionData)
    {
        // Vulnerability: Insecure deserialization via obsolete BinaryFormatter leads to RCE
#pragma warning disable SYSLIB0011
        var formatter = new BinaryFormatter();
        using var stream = new MemoryStream(sessionData);
        return formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011
    }
}
