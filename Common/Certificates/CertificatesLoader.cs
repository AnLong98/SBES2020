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
    }
}
