using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.KeyManager
{
    public class SecretKeyHandler
    {
        static readonly object syncObject = new object();

        public string GetKey(string sender)
        {
            string path = sender + "_key.txt";  //  sender_key.txt
            string key = "";

            try
            {
                lock (syncObject)
                {
                    using (FileStream fInput = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[(int)fInput.Length];
                        fInput.Read(buffer, 0, (int)fInput.Length);
                        key = ASCIIEncoding.ASCII.GetString(buffer);

                    }
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return key;

        }


        public void StoreKey(string keyOwner, string key)
        {
            string path = keyOwner + "_key.txt";  //  sender_key.txt

            try
            {
                lock (syncObject)
                {
                    using (FileStream fOut = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = Encoding.ASCII.GetBytes(key);
                        fOut.Write(buffer, 0, buffer.Length);
                    }
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public string GenerateKey()
        {
             AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
             aes.GenerateIV();
             aes.GenerateKey();
             return ASCIIEncoding.ASCII.GetString(aes.Key);

            //return ASCIIEncoding.ASCII.GetString(AesCryptoServiceProvider.Create().Key);
        }
    }
}

