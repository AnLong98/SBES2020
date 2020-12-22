using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringServer.EncryptedMessageLogger
{
    public class FindingTheKey
    {
        static readonly object syncObject = new object();

        public static string GetKey(string sender)
        {
            string path = "..." + sender + ".txt";  // dodati putanju do fajla gde se cuvaju AES lozinke klijenata (lozinka se cuva u .txt fajlu naziva odgovarajuceg klijenta)
            string key = "";

            try
            {
                lock (syncObject)
                {
                    using (StreamReader reader = File.OpenText(Path.GetFullPath(path)))
                    {
                        key = reader.ReadToEnd();
                    }
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return key;

        }


    }
}
