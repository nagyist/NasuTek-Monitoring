using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace NasuTek.Monitoring.Service
{
    class MonitorEngine
    {
        public Dictionary<string, NMonitor> Monitors { get; private set; }
        public XDocument XmlSettingsFile { get; private set; }

        public MonitorEngine()
        {
        }

        public void StartMonitors()
        {
            foreach (XElement i in XmlSettingsFile.Root.Elements("Monitor"))
            {
                Monitors.Add(i.Attribute("name").Value, new NMonitor(i));
            }
        }
    }
}
