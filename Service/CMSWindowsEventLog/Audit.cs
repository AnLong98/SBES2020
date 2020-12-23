using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CMSWindowsEventLog
{
    public class Audit : IDisposable
    {
        private static EventLog customLog = null;
        const string SourceName = "Server.CMSWindowsEventLog.Audit";
        const string LogName = "ServerEventLog";

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


        public static void CreateCertificateAndKey(string userName)
        {
            string message = AuditEvents.CreateCertificateAndKey;
            if (customLog != null)
            {
                string complete_message = String.Format(message, userName);
                customLog.WriteEntry(complete_message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.", (int)AuditEventTypes.CreateCertificateAndKey));
            }


        }

        public static void CertificateRevocated(string userName)
        {
            string message = AuditEvents.CertificateRevocated;
            if (customLog != null)
            {
                string complete_message = String.Format(message, userName);
                customLog.WriteEntry(complete_message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.", (int)AuditEventTypes.CertificateRevocated));
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
