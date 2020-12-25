using Common.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Parsers;
using Common.KeyManager;
using Service.Certificate;
using Service.GenerateKey;
using Service.CMSWindowsEventLog;

namespace Service
{
    public class WCFService : ICentralAuthServer
    {
        public void Authenticate(string ip, string port)
        {
            
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity windowsIdentity = identity as WindowsIdentity;
            string userName = WinLogonNameParser.ParseName(windowsIdentity.Name);

            // add new user to user list
            if (!Users.UserAccounts.ContainsKey(userName))
            {
                Users.UserAccounts.Add(userName, new User(ip, port, userName));

                var privKey = CertificateManager.GenerateCACertificate("CN=CentralServerCA");
                var cert = CertificateManager.GenerateSelfSignedCertificate($"CN={userName}", "CN=CentralServerCA", privKey);
                byte[] certBytes = cert.Export(X509ContentType.Pkcs12, "1234");

                string outCertPath = $"../../UserCeritifactes/{userName}";
                System.IO.Directory.CreateDirectory(Path.GetFullPath(outCertPath));

                File.WriteAllBytes(Path.Combine(outCertPath, $"{userName}.cer"), cert.Export(X509ContentType.Cert));
                File.WriteAllBytes(Path.Combine(outCertPath, $"{userName}.pfx"), cert.Export(X509ContentType.Pkcs12, "1234"));

                SecretKeyHandler skh = new SecretKeyHandler();
                string key = Generate.KeyGenerator();
                skh.StoreKey(userName, key);

                Audit.CreateCertificateAndKey(userName);
            }
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
            //Audit.CertificateRevocated(userName);
            throw new NotImplementedException();
        }
    }
}
