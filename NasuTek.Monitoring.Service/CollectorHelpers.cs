using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service
{
    public static class CollectorHelpers
    {
        public static void IsCollectorFormatterValid(Type collectorType, params string[] collectorTypeNames)
        {
            if (!collectorTypeNames.Any(v => v == collectorType.FullName))
                throw new ArgumentException("The collector type specified \"" + collectorType.FullName + "\" is not valid for this collector formatter", "type");
        }
    }
}
