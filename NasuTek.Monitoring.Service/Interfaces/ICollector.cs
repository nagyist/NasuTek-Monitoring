using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service
{
    public interface ICollector
    {
        Dictionary<string, string> ExecuteCollector(Dictionary<string, string> parameters, Dictionary<string, string> overrides);
    }
}
