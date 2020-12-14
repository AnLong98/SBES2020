using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class WCFService : ICentralAuthServer
    {
        public void Authenticate(string ip, string port)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public void RevocateCertificate()
        {
            throw new NotImplementedException();
        }
    }
}
