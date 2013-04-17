using NasuTek.Monitoring.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasuTek.Monitoring.Service.BuiltIn.Reporters
{
    public class ConsoleReporter : IReporter
    {
        public void ExecuteReport(Dictionary<string, string> parameters, Dictionary<string, Dictionary<string, string>> domains)
        {
            foreach (var domain in domains.GetSubdomains())
            {
                Console.WriteLine("Domain: " + domain.Key);

                if (domain.Value.ContainsKey("Global"))
                {
                    foreach (var value in domain.Value["Global"])
                    {
                        Console.WriteLine("\tKey: " + value.Key);
                        Console.WriteLine("\t\tValue: " + value.Value);
                    }
                }

                foreach (var subdomain in domain.Value.Where(v => v.Key != "Global"))
                {
                    Console.WriteLine("\tSubdomain: " + subdomain.Key);
                    foreach (var value in subdomain.Value)
                    {
                        Console.WriteLine("\t\tKey: " + value.Key);
                        Console.WriteLine("\t\t\tValue: " + value.Value);
                    }
                }
            }
        }
    }
}
