using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESSecurity.Tests
{
    [TestFixture]
    public class AESCryptoTests
    {
        [TestCase("Cao ja samo testiram ovo", "FFDAFA2FFBA91E5A6E50197AABC0624A")]
        public void Encrypt__Plaintext__AssertPlaintextChanged(string plaintext, string key)
        {
            AESCryptographyProvider provider = new AESCryptographyProvider();
            string encrypted = provider.Encrypt(plaintext, key);

            Assert.AreNotEqual(plaintext, encrypted);
        }

        [TestCase("Cao ja samo testiram ovo", "FFDAFA2FFBA91E5A6E50197AABC0624A")]
        public void Decyrpt__PlaintextEncrypted__AssertDecryptedToPlaintext(string plaintext, string key)
        {
            AESCryptographyProvider provider = new AESCryptographyProvider();
            string encrypted = provider.Encrypt(plaintext, key);
            string decrypted = provider.Decrypt(encrypted, key);

            Assert.AreEqual(plaintext, decrypted);
        }

    }
}
