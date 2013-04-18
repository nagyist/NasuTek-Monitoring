using NasuTek.Monitoring.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace NasuTek.Monitoring.Service.BuiltIn.Reporters
{
    public class EmailReporter : IReporter
    {
        public void ExecuteReport(Dictionary<string, string> parameters, Preprocessor.ProcessingLibrary.Processor processor)
        {
            SmtpClient client = new SmtpClient();
            client.Port = Convert.ToInt32(parameters["port"]);
            client.Host = parameters["host"];
            client.EnableSsl = Convert.ToBoolean(parameters["enableSsl"]);
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            if (parameters.ContainsKey("user"))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(parameters["user"], parameters["password"]);
            }

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(parameters["from"]);
            mm.Subject = parameters["subjectFormat"];
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }
    }
}
