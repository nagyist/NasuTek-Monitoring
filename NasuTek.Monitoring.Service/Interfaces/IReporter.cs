using NasuTek.Preprocessor.ProcessingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service.Interfaces
{
    public interface IReporter
    {
        void ExecuteReport(Dictionary<string, string> parameters, Processor processor);
    }
}
