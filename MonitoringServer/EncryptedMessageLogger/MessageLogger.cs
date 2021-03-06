﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringServer.EncryptedMessageLogger
{
    public class MessageLogger
    {
        static readonly object syncObject = new object();

        public static void LoggMessage(string s)
        {
            lock (syncObject)
            {
                using (StreamWriter sw = File.AppendText(Path.GetFullPath("../../EncryptedMessageLogger/Messages/messages.txt")))
                {
                    sw.WriteLine(s);
                }
            }
        }
        

    }
}
