using NasuTek.Monitoring.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace NasuTek.Monitoring.Service.BuiltIn.CollectorFormatters
{
    public class XmlFormatter : ICollectorFormatter
    {
        public Dictionary<string, Dictionary<string, string>> FormatCollector(Dictionary<string, string> parameters, Dictionary<string, string> collectorDict, System.Xml.Linq.XElement collectorElement, Type collectorType)
        {
            CollectorHelpers.IsCollectorFormatterValid(collectorType, "NasuTek.Monitoring.Service.BuiltIn.Collectors.FileCollector");

            switch (collectorType.FullName)
            {
                case "NasuTek.Monitoring.Service.BuiltIn.Collectors.FileCollector":
                    {
                        string[] files = collectorDict["Files"].Split(',');
                        var dictRet = new Dictionary<string, Dictionary<string, string>>();

                        foreach (var file in files)
                        {
                            var xmlDoc = XDocument.Load(file);
                            foreach (var xmlRefVal in collectorElement.Elements("XmlRefToKeyValue"))
                            {
                                CheckIfDomainExists(dictRet, xmlRefVal.Attribute("domain").Value.CreateSubdomainName(Path.GetFileName(file)));
                                XElement ele = xmlDoc.XPathSelectElement(xmlRefVal.Attribute("name").Value);
                                if (ele != null)
                                    dictRet[xmlRefVal.Attribute("domain").Value.CreateSubdomainName(Path.GetFileName(file))][xmlRefVal.Attribute("domain_key").Value] = ele.Value;
                            }
                        }

                        return dictRet;
                    }
                default:
                    return null;
            }
        }

        public void CheckIfDomainExists(Dictionary<string, Dictionary<string, string>> dict, string domain)
        {
            if (!dict.ContainsKey(domain))
                dict.Add(domain, new Dictionary<string, string>());
        }
    }
}
