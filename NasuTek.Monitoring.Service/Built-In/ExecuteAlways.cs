using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service
{
    public class ExecuteAlways : IMonitor
    {
        public bool TriggerMonitor(NMonitor monitorClassObject)
        {
            return true;
        }
    }
}
