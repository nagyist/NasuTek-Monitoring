using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static string CreateSubdomainName(this string domainName, string subdomainName)
        {
            return domainName + ":" + subdomainName;
        }

        public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> GetSubdomains(this Dictionary<string, Dictionary<string, string>> me)
        {
            var subdomains = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            foreach (var item in me.Where(v => v.Key.Split(':').Length > 1))
            {
                if (!subdomains.ContainsKey(item.Key.Split(':')[0]))
                    subdomains.Add(item.Key.Split(':')[0], new Dictionary<string, Dictionary<string, string>>());
                if (!subdomains[item.Key.Split(':')[0]].ContainsKey(item.Key.Split(':')[1]))
                    subdomains[item.Key.Split(':')[0]].Add(item.Key.Split(':')[1], new Dictionary<string, string>());

                foreach (var kv in item.Value)
                {
                    subdomains[item.Key.Split(':')[0]][item.Key.Split(':')[1]].Add(kv.Key, kv.Value);
                }
            }

            foreach (var item in me.Where(v => v.Key.Split(':').Length == 1))
            {
                if (!subdomains.ContainsKey(item.Key.Split(':')[0]))
                    subdomains.Add(item.Key, new Dictionary<string, Dictionary<string, string>>());
                if (!subdomains[item.Key].ContainsKey("Global"))
                    subdomains[item.Key].Add("Global", new Dictionary<string, string>());

                foreach (var kv in item.Value)
                {
                    subdomains[item.Key]["Global"].Add(kv.Key, kv.Value);
                }
            }

            return subdomains;
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
