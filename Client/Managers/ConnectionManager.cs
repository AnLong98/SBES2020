using Client.Contracts;
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
            string address = "net.tcp://localhost:9999/Service";

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
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IClient> channelFactory = new ChannelFactory<IClient>(binding, "net.tcp://127.0.0.1:0/Client");
            channelFactory.Endpoint.Address = new EndpointAddress($"net.tcp://{clientIP}:{clientPort}/Client");
            return channelFactory.CreateChannel();

        }
    }
}
