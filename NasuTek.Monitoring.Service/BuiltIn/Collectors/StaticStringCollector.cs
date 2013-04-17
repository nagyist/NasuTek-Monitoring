using NasuTek.Monitoring.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service.BuiltIn.Collectors
{
    public class StaticStringCollector : ICollector
    {
        public Dictionary<string, string> ExecuteCollector(Dictionary<string, string> parameters, Dictionary<string, string> overrides)
        {
            return parameters;
        }
    }
}
