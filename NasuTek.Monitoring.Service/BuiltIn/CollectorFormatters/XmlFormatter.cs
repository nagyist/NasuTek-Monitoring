using NasuTek.Monitoring.Service.Interfaces;
using NasuTek.Preprocessor.ProcessingLibrary;
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
        public void FormatCollector(Dictionary<string, string> parameters, Dictionary<string, string> collectorDict, System.Xml.Linq.XElement collectorElement, Type collectorType, Processor processor)
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
                                processor.AddDomain(xmlRefVal.Attribute("domain").Value, Path.GetFileName(file));

                                XElement ele = xmlDoc.XPathSelectElement(xmlRefVal.Attribute("name").Value);
                                if (ele != null)
                                    processor.GetDomain(xmlRefVal.Attribute("domain").Value, Path.GetFileName(file))[xmlRefVal.Attribute("domain_key").Value] = ele.Value;
                            }
                        }
                    }
                    break;
            }
        }        
    }
}
