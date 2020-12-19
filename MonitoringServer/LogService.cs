using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringServer
{
    public class LogService : IMonitoringServer
    {
        public void LogCommunication(string sender, string receiver, string timestamp, string message)
        {
            string log = "";
            log = $"[{timestamp}]\tsender: {sender}\treceiver: {receiver}\tmessage: \"{message}\"";
            MessageLogger.LoggMessage(log);

        }

        public void LogCommunicationEnd(string sender, string receiver, string timestamp)
        {
            throw new NotImplementedException();
        }

        public void LogCommunicationStart(string sender, string receiver, string timestamp)
        {
            throw new NotImplementedException();
        }

    }
}
