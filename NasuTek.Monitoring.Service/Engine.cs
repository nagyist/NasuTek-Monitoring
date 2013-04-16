using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace NasuTek.Monitoring.Service
{
    class Engine
    {
        public Dictionary<string, NMonitor> Monitors { get; private set; }

        public void Start()
        {
            XDocument doc = XDocument.Load(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "NasuTek.Monitoring.Service.config"));

            Monitors = new Dictionary<string, NMonitor>();

            foreach (XElement assemblyImport in doc.Root.Elements("Import"))
            {
                Assembly.LoadFile(assemblyImport.Attribute("dll").Value);
            }

            foreach(XElement monitor in doc.Root.Elements("Monitor"))
            {
                var mntr = new NMonitor(monitor);
                Monitors.Add(monitor.Attribute("name").Value, mntr);
                mntr.Start();
            }
        }
    }
}
