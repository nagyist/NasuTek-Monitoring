using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NasuTek.Monitoring.Service.Interfaces
{
    public interface ICollectorFormatter
    {
        Dictionary<string, Dictionary<string, string>> FormatCollector(Dictionary<string, string> parameters, Dictionary<string, string> collectorDict, XElement collectorElement, Type collectorType);
    }
}
