using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Parsers;

namespace Service
{
    public class WCFService : ICentralAuthServer
    {
        public void Authenticate(string ip, string port)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;
            string userName = WinLogonNameParser.ParseName(windowsIdentity.Name);

            if (!Users.UserAccounts.ContainsKey(userName))
                Users.UserAccounts.Add(userName, new User(ip, port, userName));
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            IIdentity id = Thread.CurrentPrincipal.Identity;
            WindowsIdentity winId = id as WindowsIdentity;
            string userName = WinLogonNameParser.ParseName(winId.Name);

            foreach(KeyValuePair<string, User> kp in Users.UserAccounts) 
            {
                if(!kp.Key.Equals(userName)) //added all clients to the list / except the client we are sending this list to.
                {
                    users.Add(kp.Value);
                }
            }

            return users;
        }

        public void RevocateCertificate()
        {
            throw new NotImplementedException();
        }
    }
}
