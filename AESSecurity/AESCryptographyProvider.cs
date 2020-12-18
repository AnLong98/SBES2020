using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AESSecurity
{
    public class AESCryptographyProvider : IAESSecurity
    {
        public string Decrypt(string cyphertext, string key)
        {
            byte[] encryptedByteArray = Convert.FromBase64String(cyphertext);
            byte[] decryptedByteArray = null;

            AesCryptoServiceProvider aesCryptoProvider = new AesCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            };

            aesCryptoProvider.IV = encryptedByteArray.Take(aesCryptoProvider.BlockSize / 8).ToArray();

            ICryptoTransform cryptoTransform = aesCryptoProvider.CreateDecryptor();

            using (MemoryStream memoryStream = new MemoryStream(encryptedByteArray.Skip(aesCryptoProvider.BlockSize / 8).ToArray()))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                {
                    decryptedByteArray = new byte[encryptedByteArray.Length - aesCryptoProvider.BlockSize / 8];
                    cryptoStream.Read(decryptedByteArray, 0, decryptedByteArray.Length);
                }
            }

            return Convert.ToBase64String(decryptedByteArray);
        }

        public string Encrypt(string plaintext, string key)
        {
            byte[] encryptedByteArray;
            byte[] plaintextByteArray = Convert.FromBase64String(plaintext);

            AesCryptoServiceProvider aesCryptoProvider = new AesCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            };

            aesCryptoProvider.GenerateIV(); //create init vector at random

            ICryptoTransform cryptoTransform = aesCryptoProvider.CreateEncryptor();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plaintextByteArray, 0, plaintext.Length);
                    encryptedByteArray = aesCryptoProvider.IV.Concat(memoryStream.ToArray()).ToArray();    //encrypted image body with IV
                }
            }

            return Convert.ToBase64String(encryptedByteArray);
        }
    }
}
