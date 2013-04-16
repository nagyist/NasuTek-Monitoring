using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service
{
    public interface IReporter
    {
        void ExecuteReport(Dictionary<string, string> parameters, Dictionary<string, Dictionary<string, string>> domains);
    }
}
