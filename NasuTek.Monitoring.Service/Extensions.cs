using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NasuTek.Monitoring.Service
{
    public static class DictionaryExtensionMethods
    {
        public static void Merge(this Dictionary<string, Dictionary<string, string>> me, Dictionary<string, Dictionary<string, string>> merge)
        {
            foreach (var item in merge)
            {
                if (me.ContainsKey(item.Key))
                    me[item.Key].Merge(item.Value);
                else
                    me[item.Key] = item.Value;
            }
        }

        public static Dictionary<string, string> GetParameterDictionary(this IEnumerable<XElement> enumerable)
        {
            var paramDict = new Dictionary<string, string>();
            foreach (XElement param in enumerable)
            {
                if (param.Attribute("value") != null)
                    paramDict.Add(param.Attribute("name").Value, param.Attribute("value").Value);
                else
                    paramDict.Add(param.Attribute("name").Value, param.Value);
            }
            return paramDict;
        }

        public static void Merge(this Dictionary<string, string> me, Dictionary<string, string> merge)
        {
            foreach (var item in merge)
            {
                me[item.Key] = item.Value;
            }
        }
    }
}
