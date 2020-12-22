using MonitoringServer.EncryptedMessageLogger;
using MonitoringServer.EventLogger;
using AESSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.KeyManager;

namespace MonitoringServer
{
    public class MonitoringServiceProvider : IMonitoringServer
    {
        public void LogCommunication(string sender, string receiver, string timestamp, string message)
        {
            string key = "";
            FindingTheKey find = new FindingTheKey();
            key = find.GetKey(sender);

            string decrypted_message = "";
            AESCryptographyProvider aes = new AESCryptographyProvider();
            try
            {
                decrypted_message = aes.Decrypt(message, key);

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            string log = "";
            log = $"[{timestamp}]\tsender: {sender}\treceiver: {receiver}\tmessage: \"{decrypted_message}\"";
            MessageLogger.LoggMessage(log);

        }

        public void LogCommunicationEnd(string sender, string receiver)
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

        public void LogCommunicationStart(string sender, string receiver)
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
