using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AESSecurity.Tests
{
    [TestFixture]
    public class AESCryptoTests
    {
        [TestCase("Cao ja samo testiram ovo")]
        public void Encrypt__Plaintext__AssertPlaintextChanged(string plaintext)
        {
            string key = ASCIIEncoding.ASCII.GetString(AesCryptoServiceProvider.Create().Key);
            AESCryptographyProvider provider = new AESCryptographyProvider();
            string encrypted = provider.Encrypt(plaintext, key);

            Assert.AreNotEqual(plaintext, encrypted);
        }

        [TestCase("Cao ja samo testiram ovo")]
        public void Decyrpt__PlaintextEncrypted__AssertDecryptedToPlaintext(string plaintext)
        {
            AESCryptographyProvider provider = new AESCryptographyProvider();
            string key = ASCIIEncoding.ASCII.GetString(AesCryptoServiceProvider.Create().Key);
            string encrypted = provider.Encrypt(plaintext, key);
            string decrypted = provider.Decrypt(encrypted, key);

            Assert.AreEqual(plaintext, decrypted);
        }

    }
}
