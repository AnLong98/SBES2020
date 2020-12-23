using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Service.CMSWindowsEventLog
{
    public enum AuditEventTypes
    {
        CertificateRevocated = 0,
        CreateCertificateAndKey = 1,
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

        public static string CreateCertificateAndKey
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.CreateCertificateAndKey.ToString());

            }
        }

        public static string CertificateRevocated
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.CertificateRevocated.ToString());

            }
        }


    }
   
}
