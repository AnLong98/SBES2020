using Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Managers
{
    public class MessageNotificationManager
    {
        private static MessageNotificationManager instance;
        private Dictionary<string, IMessageNotifiable> messageReceiversDict;

        public MessageNotificationManager()
        {
            messageReceiversDict = new Dictionary<string, IMessageNotifiable>();
        }

        public static MessageNotificationManager Instance()
        {
            if (instance == null)
                instance = new MessageNotificationManager();
            return instance;
        }

        public void AddReceiver(IMessageNotifiable messageNotifiable, string senderName)
        {
            if(!messageReceiversDict.ContainsKey(senderName))
            {
                messageReceiversDict.Add(senderName, messageNotifiable);
            }
        }

        public void RemoveReceiver(string senderName)
        {
            if (messageReceiversDict.ContainsKey(senderName))
            {
                messageReceiversDict.Remove(senderName);
            }
        }

        public void NotifyReceiver(string sender, string message)
        {
            if (messageReceiversDict.ContainsKey(sender))
            {
                messageReceiversDict[sender].Notify(message);
            }
        }


    }
}
