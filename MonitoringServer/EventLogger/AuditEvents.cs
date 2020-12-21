using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringServer.EventLogger
{
    public enum AuditEventTypes
    {
        CommunicationStart = 0,
        CommunicationEnd = 1,
    }

    public class AuditEvents
    {
        private static ResourceManager resourceManager = null;
        private static object syncObject = new object();

        private static ResourceManager ResourceMgr
        {
            get
            {
                lock (syncObject)
                {
                    if (resourceManager == null)
                    {
                        resourceManager = new ResourceManager(typeof(AuditEventFile).FullName, Assembly.GetExecutingAssembly());
                    }   
                    
                    return resourceManager;
                }
            }
        }

        public static string CommunicationStart
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.CommunicationStart.ToString());
                                                                                               
            }
        }

        public static string CommunicationEnd
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.CommunicationEnd.ToString());

            }
        }



    }
}
