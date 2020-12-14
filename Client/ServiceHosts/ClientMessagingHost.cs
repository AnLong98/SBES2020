using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceHosts
{
    public class ClientMessagingHost
    {
        private NetTcpBinding binding = new NetTcpBinding();
        private string address = "net.tcp://localhost:9989/Client";
        private ServiceHost;

        public ClientMessagingHost()
        {
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
        }
        

			
        /*ServiceHost host = new ServiceHost(typeof(WCFService));
        host.AddServiceEndpoint(typeof(IWCFContract), binding, address);

			///Custom validation mode enables creation of a custom validator - CustomCertificateValidator
			host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
			host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

        ///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
        host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

			///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
			host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);*/

    }
}
