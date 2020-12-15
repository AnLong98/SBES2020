using Client.Service_Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client.ServiceHosts
{
    public class ClientMessagingHost
    {
        private NetTcpBinding binding = new NetTcpBinding();
        private string address = "net.tcp://localhost:9989/Client";
        private ServiceHost host;

        public ClientMessagingHost()
        {
            //binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            host = new ServiceHost(typeof(MessageReceivingService));
            host.AddServiceEndpoint(typeof(IClient), binding, address);

           /* host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);*/
        }

        public bool Open()
        {
            try
            {
                host.Open();
                return true;
            }catch(Exception e)
            {
                //handle
            }

            return false;
        }


        public bool Close()
        {
            try
            {
                host.Close();
                return true;
            }
            catch (Exception e)
            {
                //handle
            }

            return false;
        }

    }
}
