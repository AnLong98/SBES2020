using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.Managers
{
    public class ConnectionManager : IConnectionManager
    {
        public ICentralAuthServer GetAuthServerProxy()
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/AuthServer"; //Change thi after others implement

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address));
            ChannelFactory<ICentralAuthServer> serverChannel = new ChannelFactory<ICentralAuthServer>(binding, endpointAddress);
            return serverChannel.CreateChannel();

        }

        public IMonitoringServer GetMonitorProxy()
        {
            return null;

        }

        public IClient GetClientProxy(string clientIP, string clientPort)
        {
            return null;

        }
    }
}
