using NasuTek.Monitoring.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service.BuiltIn.Monitors
{
    public class OnNewFiles : IMonitor
    {
        public bool TriggerMonitor(NMonitor monitorClassObject)
        {
            var fileListDir = Directory.GetFiles(monitorClassObject.Parameters["Directory"]);
            if (!monitorClassObject.Processor.Storage.ContainsKey("FilesRead"))
                monitorClassObject.Processor.Storage["FilesRead"] = new List<string>(fileListDir);
            var fileList = (List<string>)monitorClassObject.Processor.Storage["FilesRead"];
            var unexecuted = fileListDir.Where(v => !fileList.Contains(v)).ToArray();
            if (unexecuted.Length == 0)
                return false;

            fileList.AddRange(unexecuted);
            monitorClassObject.Processor.Storage["FilesRead"] = fileList;

            monitorClassObject.CreateOverride("NasuTek.Monitoring.Service.BuiltIn.Collectors.FileCollector");
            monitorClassObject.Overrides["NasuTek.Monitoring.Service.BuiltIn.Collectors.FileCollector"]["Files"] = String.Join(",", unexecuted);
            return true;
        }
    }
}
