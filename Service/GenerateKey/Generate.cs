using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.GenerateKey
{
    public class Generate
    {
        public static string KeyGenerator()
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.GenerateIV();
            aes.GenerateKey();
            return ASCIIEncoding.ASCII.GetString(aes.Key);

            //return ASCIIEncoding.ASCII.GetString(AesCryptoServiceProvider.Create().Key);
        }
    }
}
