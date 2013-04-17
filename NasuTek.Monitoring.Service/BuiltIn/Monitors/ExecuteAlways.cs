using NasuTek.Monitoring.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service.BuiltIn.Monitors
{
    public class ExecuteAlways : IMonitor
    {
        public bool TriggerMonitor(NMonitor monitorClassObject)
        {
            return true;
        }
    }
}
