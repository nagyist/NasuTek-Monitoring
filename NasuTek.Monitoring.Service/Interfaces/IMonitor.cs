using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service.Interfaces
{
    public interface IMonitor
    {
        bool TriggerMonitor(NMonitor monitorClassObject);
    }
}
