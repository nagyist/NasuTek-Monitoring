using NasuTek.Preprocessor.ProcessingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NasuTek.Monitoring.Service.Interfaces
{
    public interface ICollectorFormatter
    {
        void FormatCollector(Dictionary<string, string> parameters, Dictionary<string, string> collectorDict, XElement collectorElement, Type collectorType, Processor processor);
    }
}
