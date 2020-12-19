using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringServer
{
    public class MessageLogger
    {
        public static void LoggMessage(string s)
        {
            string path = System.IO.Path.GetFullPath("../../Messages/messages.txt");
            FileStream stream = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);

            sw.WriteLine(s);

            sw.Close();
            stream.Close();

        }
        
    }
}
