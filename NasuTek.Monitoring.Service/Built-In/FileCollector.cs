using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service
{
    public class FileCollector : ICollector
    {
        public Dictionary<string, string> ExecuteCollector(Dictionary<string, string> parameters, Dictionary<string, string> overrides)
        {
            if (overrides.ContainsKey("Files"))
                return new Dictionary<string, string> { { "Files", overrides["Files"] } };
                
            string[] files = Directory.GetFiles(parameters["Directory"]);
            return new Dictionary<string, string> { { "Files", String.Join(",", files) } };
        }
    }
}
