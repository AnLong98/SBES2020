using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common.Certificates
{
    public class CertificatesLoader
    {
        public static X509Certificate2 GetCertificateFromFile(string file)
        {
            string certificatePath = file;
            X509Certificate2 certificate = null;

            certificate = new X509Certificate2(certificatePath, "1234");
            certificate.Import(certificatePath);


            return certificate;
        }

        public static X509Certificate2 GetCertificateFromStore(string ownerName, StoreName storeName, StoreLocation location)
        {
            X509Store store = new X509Store(storeName, location);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindBySubjectName, ownerName, true);

            foreach (X509Certificate2 c in certCollection)
            {
                if (c.SubjectName.Name.Equals(string.Format("CN={0}", ownerName)))
                {
                    return c;
                }
            }

            return null;
        }
    }
}
