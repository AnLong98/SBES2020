using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringServer.EventLogger
{
    public class Audit : IDisposable
    {
        private static EventLog customLog = null;
        const string SourceName = "MonitoringServer.Audit";
        const string LogName = "Inter-Client Communication";

        static Audit()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);  
                }
                customLog = new EventLog(LogName, Environment.MachineName, SourceName);

            }
            catch (Exception e)
            {
                customLog = null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
        }


        public static void CommunicationStart(string client1, string client2)
        {
            string message = AuditEvents.CommunicationStart;
            if (customLog != null)
            {
                string complete_message = String.Format(message, client1, client2);
                customLog.WriteEntry(complete_message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.", (int)AuditEventTypes.CommunicationStart));
            }


        }

        public static void CommunicationEnd(string client1, string client2)
        {
            string message = AuditEvents.CommunicationStart;
            if (customLog != null)
            {
                string complete_message = String.Format(message, client1, client2);
                customLog.WriteEntry(complete_message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.", (int)AuditEventTypes.CommunicationStart));
            }
        }
        

        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }

    }
}
