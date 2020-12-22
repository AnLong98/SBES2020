using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.KeyManager
{
    public class FindingTheKey
    {
        static readonly object syncObject = new object();

        public string GetKey(string sender)
        {
            string path = "../../../Service/Certificates/" + sender + "/" + sender + "_key.txt";  //  Service/Certificates/sender/sender_key.txt
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
