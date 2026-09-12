using System.Xml;

namespace DotnetBenchmark.Services;

public class XmlService
{
    // XXE vulnerability: XmlDocument without entity resolution disabled
    public void ParseXml(string xmlContent)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xmlContent);
    }
}
