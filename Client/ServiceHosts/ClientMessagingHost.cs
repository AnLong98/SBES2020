using Client.Service_Providers;
using Common.Certificates;
using Common.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading;

namespace Client.ServiceHosts
{
    public class ClientMessagingHost
    {
        private readonly string address = "net.tcp://127.0.0.1:0/Client";
        private NetTcpBinding binding = new NetTcpBinding();
        private ServiceHost host;

        public ClientMessagingHost()
        {
        }

        public void CreateService()
        {
            string userName = WinLogonNameParser.ParseName(WindowsIdentity.GetCurrent().Name);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            host = new ServiceHost(typeof(MessageReceivingService));
            host.AddServiceEndpoint(typeof(IClient), binding, address);
            host.Description.Endpoints[0].ListenUriMode = System.ServiceModel.Description.ListenUriMode.Unique;

            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            host.Credentials.ServiceCertificate.Certificate = CertificatesLoader.GetCertificateFromStore(userName, StoreName.My, StoreLocation.LocalMachine);
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

        public int GetPort()
        {
            return host.ChannelDispatchers[0].Listener.Uri.Port;
        }

        public string GetIP()
        {
            return "127.0.0.1";
        }
        
    }
}
