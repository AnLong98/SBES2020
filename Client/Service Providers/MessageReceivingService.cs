using AESSecurity;
using Client.Managers;
using Common.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Service_Providers
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, IncludeExceptionDetailInFaults = true)]
    public class MessageReceivingService : IClient
    {
        public void SendCommunicationRequest(string ownIP, string port, string ownUserName)
        {

            string currentUser = WinLogonNameParser.ParseName(WindowsIdentity.GetCurrent().Name);
            if (!MessageNotificationManager.Instance().CheckExists(ownUserName))
            {
                //TODO: Change this awful code and architecture to something better
                ConnectionManager connManager = new ConnectionManager();
                AESCryptographyProvider cryptographyProvider = new AESCryptographyProvider();
                ChatWindowManager.CreateNewChatWindow(ownUserName, currentUser, connManager.GetClientProxy(ownIP, port, ownUserName), connManager.GetMonitorProxy(), cryptographyProvider);
            }
        }

        public void SendMessage(string message)
        {
            string sender = WinLogonNameParser.ParseName(ServiceSecurityContext.Current.WindowsIdentity.Name);
            MessageNotificationManager.Instance().NotifyReceiver(sender, message);
        }
    }
}
