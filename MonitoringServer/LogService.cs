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
            throw new NotImplementedException();
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
