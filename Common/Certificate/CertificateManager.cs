using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common.Certificate
{
    public static class CertificateManager
    {
        public static X509Certificate2 CreateCertificate(string userName)
        {
            ///makecert -sv WCFService.pvk -iv TestCA.pvk -n "CN=wcfservice" -pe -ic TestCA.cer WCFService.cer -sr localmachine -ss My -sky exchange
            ///pvk2pfx.exe /pvk WCFService.pvk /pi 1234 /spc WCFService.cer /pfx WCFService.pfx

            //const string MakeCert = "C:\\Program Files\\Microsoft Visual Studio 8\\Common7\\Tools\\Bin\\makecert.exe";
            const string MakeCert = @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64\makecert.exe";

            //string fileName = Path.ChangeExtension(Path.GetTempFileName(), "cer");
            string fileName = Path.ChangeExtension(@"C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64\" +userName, "cer");
            //string userName = Guid.NewGuid().ToString();

            /// if -sk is used instead of -sv, we don't get any password popup
            string arguments = 
                string.Format("-sv {0}.pvk -iv CentralServerCA.pvk -n \"CN={0}\" -pe -ic CentralServerCA.cer {0}.cer -sr localmachine -ss My -sky exchange",
                userName);

            Process p = Process.Start(MakeCert, arguments);
            p.WaitForExit();

            //byte[] certBytes = ReadFile(fileName);
            X509Certificate2 cert = new X509Certificate2();
            return cert;
        }

        // read generated .cer
        private static byte[] ReadFile(string fileName)
        {
            using (FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                int size = (int)f.Length;
                byte[] data = new byte[size];
                size = f.Read(data, 0, size);
                return data;
            }
        }
    }
}
