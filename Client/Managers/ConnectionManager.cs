using Client.Contracts;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Managers
{
    public class ConnectionManager : IConnectionManager
    {
        public ICentralAuthServer GetAuthServerProxy()
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9998/Service";

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address));
            ChannelFactory<ICentralAuthServer> serverChannel = new ChannelFactory<ICentralAuthServer>(binding, endpointAddress);
            return serverChannel.CreateChannel();

        }

        public IMonitoringServer GetMonitorProxy()
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/Monitoring";

            ChannelFactory<IMonitoringServer> serverChannel = new ChannelFactory<IMonitoringServer>(binding, address);
            return serverChannel.CreateChannel();

        }

        public IClient GetClientProxy(string clientIP, string clientPort, string clientUserName)
        {
            string cltCertCN = WinLogonNameParser.ParseName(WindowsIdentity.GetCurrent().Name);

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            X509Certificate2 cltCert = CertificatesLoader.GetCertificateFromStore(clientUserName, StoreName.TrustedPeople, StoreLocation.LocalMachine);
            ChannelFactory<IClient> channelFactory = new ChannelFactory<IClient>(binding, "net.tcp://127.0.0.1:0/Client");
            channelFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;
            channelFactory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            channelFactory.Credentials.ClientCertificate.Certificate = CertificatesLoader.GetCertificateFromStore(cltCertCN, StoreName.My, 
                                                                                                                  StoreLocation.LocalMachine);
            X509CertificateEndpointIdentity serviceIdentity = new X509CertificateEndpointIdentity(cltCert);
            channelFactory.Endpoint.Address = new EndpointAddress(new Uri($"net.tcp://{clientIP}:{clientPort}/Client"),
                                                                  serviceIdentity);
            return channelFactory.CreateChannel();

        }
    }
}
