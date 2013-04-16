using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace NasuTek.Monitoring.Service
{
    public class XmlFormatter : ICollectorFormatter
    {
        public Dictionary<string, Dictionary<string, string>> FormatCollector(Dictionary<string, string> parameters, Dictionary<string, string> collectorDict, System.Xml.Linq.XElement collectorElement)
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

        public void CheckIfDomainExists(Dictionary<string, Dictionary<string, string>> dict, string domain)
        {
            if (!dict.ContainsKey(domain))
                dict.Add(domain, new Dictionary<string, string>());
        }
    }
}
