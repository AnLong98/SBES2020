using Common.KeyManager;
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
        private AESCryptographyProvider provider = new AESCryptographyProvider();
        private SecretKeyHandler keyHandler = new SecretKeyHandler();

        [TestCase("Cao ja samo testiram ovo")]
        public void Encrypt__Plaintext__AssertPlaintextChanged(string plaintext)
        {
            string key = ASCIIEncoding.ASCII.GetString(AesCryptoServiceProvider.Create().Key);
            string encrypted = provider.Encrypt(plaintext, key);

            Assert.AreNotEqual(plaintext, encrypted);
        }

        [TestCase("Cao ja samo testiram ovo бак1 м0ј д0бр1")]
        public void Decyrpt__PlaintextEncrypted__AssertDecryptedToPlaintext(string plaintext)
        {
            string key = ASCIIEncoding.ASCII.GetString(AesCryptoServiceProvider.Create().Key);
            string encrypted = provider.Encrypt(plaintext, key);
            string decrypted = provider.Decrypt(encrypted, key);

            Assert.AreEqual(plaintext, decrypted);
        }

        [TestCase("Cao ja samo testiram ovo бак1 м0ј д0бр1")]
        public void Decyrpt__PlaintextEncryptedKeyStoredIntegration__AssertDecryptedToPlaintext(string plaintext)
        {
            string key = ASCIIEncoding.ASCII.GetString(AesCryptoServiceProvider.Create().Key);
            //Store generated key in file
            keyHandler.StoreKey("Pedjanerka", key);
            //Load the key from file
            string keyLoaded = keyHandler.GetKey("Pedjanerka");
            //Encrypt the damn thing
            string encrypted = provider.Encrypt(plaintext, keyLoaded);
            //Load the key again from file
            string keyReLoaded = keyHandler.GetKey("Pedjanerka");
            //Decrypt the thing with the reloaded key
            string decrypted = provider.Decrypt(encrypted, keyReLoaded);

            Assert.AreEqual(plaintext, decrypted);
        }

    }
}
