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
            // pozvati metodu koja ce mi vratiti odgovarajuci kljuc: 
            // posaljem joj sender, ona nadje fajl koji se zove sender, procita ga i vrati to (key)
            // ako ne postoji vratiti gresku o tome throw new FileNotFoundException
            // ovde ga uhvatiti, ako ne bude htelo => onda ga vatati tamo gde citam...
            
            // pozvati ovu metodu u najobicnijem try-catch bloku(Exception e)
            // pozovem: Encrypt(message, key)

            string log = "";
            log = $"[{timestamp}]\tsender: {sender}\treceiver: {receiver}\tmessage: \"{message}\"";
            //MessageLogger.LoggMessage(log);

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
