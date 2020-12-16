using Client.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service_Providers
{
    public class MessageReceivingService : IClient
    {
        public void sendMessage(string message)
        {
            string sender = ParseName(ServiceSecurityContext.Current.WindowsIdentity.Name);
            if(!MessageNotificationManager.Instance().CheckExists(sender))
            {
                //TODO: Change this awful code and architecture to something better
                OperationContext context = OperationContext.Current;
                MessageProperties prop = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                string ip = endpoint.Address;
                string port = endpoint.Port.ToString();
                ConnectionManager connManager = new ConnectionManager();
                ChatWindowManager.CreateNewForIncoming(sender, WindowsIdentity.GetCurrent().Name, connManager.GetClientProxy(ip, port));
            }
            MessageNotificationManager.Instance().NotifyReceiver(sender, message);
        }

        private string ParseName(string winLogonName)
        {
            string[] parts = new string[] { };

            if (winLogonName.Contains("@"))
            {
                ///UPN format
                parts = winLogonName.Split('@');
                return parts[0];
            }
            else if (winLogonName.Contains("\\"))
            {
                /// SPN format
                parts = winLogonName.Split('\\');
                return parts[1];
            }
            else
            {
                return winLogonName;
            }
        }


    }
}
