using MonitoringServer.EncryptedMessageLogger;
using MonitoringServer.EventLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringServer
{
    public class MonitoringServiceProvider : IMonitoringServer
    {
        public void LogCommunication(string sender, string receiver, string timestamp, string message)
        {
            // encrypt...

            string log = "";
            log = $"[{timestamp}]\tsender: {sender}\treceiver: {receiver}\tmessage: \"{message}\"";
            MessageLogger.LoggMessage(log);

        }

        public void LogCommunicationEnd(string sender, string receiver, string timestamp)
        {
            try
            {
                Audit.CommunicationEnd(sender, receiver);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void LogCommunicationStart(string sender, string receiver, string timestamp)
        {
            try
            {
                Audit.CommunicationStart(sender, receiver);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
