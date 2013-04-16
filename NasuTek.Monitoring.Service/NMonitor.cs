using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace NasuTek.Monitoring.Service
{
    public class NMonitor
    {
        protected Thread MonitorThread { get; private set; }
        public string Name { get; private set; }
        public Dictionary<string, string> Parameters { get; private set; }
        protected IMonitor MonitorObject { get; private set; }
        protected bool TerminateThread { get; set; }
        protected TimeSpan WaitTime { get; private set; }
        protected XElement ElementObject { get; set; }
        public Dictionary<string, object> Storage { get; private set; }
        public Dictionary<string, Dictionary<string, string>> Overrides { get; private set; }

        public NMonitor(XElement element)
        {
            Parameters = new Dictionary<string, string>();
            Overrides = new Dictionary<string, Dictionary<string, string>>();
            Storage = new Dictionary<string, object>();
            Name = element.Attribute("name").Value;
            MonitorThread = new Thread(new ParameterizedThreadStart(MonitorThreadCaller));
            MonitorThread.Name = Name;
            TerminateThread = false;
            WaitTime = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(element.Attribute("time").Value));
            foreach (XElement i in element.Elements("MonitorParam"))
            {
                Parameters.Add(i.Attribute("name").Value, i.Attribute("value").Value);
            }
            MonitorObject = GetObject<IMonitor>(element.Attribute("type").Value);
            ElementObject = element;
        }

        public void Start()
        {
            MonitorThread.Start(null);
        }

        public void Stop()
        {
            TerminateThread = true;
        }

        public void MonitorThreadCaller(object obj)
        {
            while (!TerminateThread)
            {
                if (MonitorObject.TriggerMonitor(this))
                {
                    var repDict = new Dictionary<string, Dictionary<string, string>>();
                    foreach (XElement collector in ElementObject.Elements("Collector"))
                    {
                        var paramDict = new Dictionary<string, string>();
                        foreach (XElement param in collector.Elements("CollectorParam"))
                        {
                            paramDict.Add(param.Attribute("name").Value, param.Attribute("value").Value);
                        }
                        ICollector collectorObject = GetObject<ICollector>(collector.Attribute("type").Value);
                        var cdl = collectorObject.ExecuteCollector(paramDict, Overrides[collector.Attribute("type").Value]);

                        foreach (XElement collectorFormatter in collector.Elements("CollectorFormatter"))
                        {
                            var paramDictFmmtr = new Dictionary<string, string>();
                            foreach (XElement param in collectorFormatter.Elements("CollectorFormatterParam"))
                            {
                                paramDictFmmtr.Add(param.Attribute("name").Value, param.Attribute("value").Value);
                            }
                            ICollectorFormatter collectorFormatterObject = GetObject<ICollectorFormatter>(collectorFormatter.Attribute("type").Value);
                            var retVal = collectorFormatterObject.FormatCollector(paramDictFmmtr, cdl, collectorFormatter);
                            repDict.Merge(retVal);
                        }
                    }

                    foreach (XElement reporter in ElementObject.Elements("Reporter"))
                    {
                        var paramDict = new Dictionary<string, string>();
                        foreach (XElement param in reporter.Elements("ReporterParam"))
                        {
                            paramDict.Add(param.Attribute("name").Value, param.Attribute("value").Value);
                        }
                        var format = reporter.Element("Format");
                        if (format != null)
                            paramDict.Add("Format", format.Value);
                        IReporter collectorFormatterObject = GetObject<IReporter>(reporter.Attribute("type").Value);
                        collectorFormatterObject.ExecuteReport(paramDict, repDict);
                    }
                }
                Thread.Sleep(WaitTime);
            }
        }

        private T GetObject<T>(string p)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetType(p) != null);
            return (T)assembly.GetType(p).GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
        }
    }
}
