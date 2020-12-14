using Client.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service_Providers
{
    public class MessageReceivingService : IClient
    {
        public void sendMessage(string message)
        {
            string sender = ParseName(ServiceSecurityContext.Current.WindowsIdentity.Name);
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
