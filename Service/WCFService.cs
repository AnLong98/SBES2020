using Common.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class WCFService : ICentralAuthServer
    {
        public void Authenticate(string ip, string port)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;
            string userName = WinLogonNameParser.ParseName( windowsIdentity.Name);


            if (!Users.UserAccounts.ContainsKey(userName))
                Users.UserAccounts.Add(userName, new User(ip, port, userName));
        }

        public List<User> GetAllUsers()
        {
            return Users.UserAccounts.Values.ToList();
        }

        public void RevocateCertificate()
        {
            throw new NotImplementedException();
        }
    }
}
