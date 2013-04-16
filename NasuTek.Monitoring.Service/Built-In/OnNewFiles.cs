using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service
{
    public class OnNewFiles : IMonitor
    {
        public bool TriggerMonitor(NMonitor monitorClassObject)
        {
            var fileListDir = Directory.GetFiles(monitorClassObject.Parameters["Directory"]);
            if (!monitorClassObject.Storage.ContainsKey("FilesRead"))
                monitorClassObject.Storage["FilesRead"] = new List<string>(fileListDir);
            var fileList = (List<string>)monitorClassObject.Storage["FilesRead"];
            var unexecuted = fileListDir.Where(v => !fileList.Contains(v)).ToArray();
            if (unexecuted.Length == 0)
                return false;

            fileList.AddRange(unexecuted);
            monitorClassObject.Storage["FilesRead"] = fileList;
            if (!monitorClassObject.Overrides.ContainsKey("NasuTek.Monitoring.Service.FileCollector")) 
                monitorClassObject.Overrides.Add("NasuTek.Monitoring.Service.FileCollector", new Dictionary<string, string>());
            monitorClassObject.Overrides["NasuTek.Monitoring.Service.FileCollector"]["Files"] = String.Join(",", unexecuted);
            return true;
        }
    }
}
