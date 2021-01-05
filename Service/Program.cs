using Common.KeyManager;
using Service.Certificate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch("Switch.System.Security.Cryptography.Xml.UseInsecureHashAlgorithms", true);
            AppContext.SetSwitch("Switch.System.Security.Cryptography.Pkcs.UseInsecureHashAlgorithms", true);

            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9998/Service";

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            ServiceHost host = new ServiceHost(typeof(WCFService));
            host.AddServiceEndpoint(typeof(ICentralAuthServer), binding, address);

            ServiceSecurityAuditBehavior newAudit = new ServiceSecurityAuditBehavior();
            newAudit.AuditLogLocation = AuditLogLocation.Application;
            newAudit.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;

            host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
            host.Description.Behaviors.Add(newAudit);

            host.Open();

            Console.WriteLine("Servis je pokrenut.");

            CertificateManager.GenerateCRL();
            Console.ReadLine();
        }
    }
}
