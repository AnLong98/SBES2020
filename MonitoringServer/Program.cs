using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringServer
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/Monitoring";
            ServiceHost host = new ServiceHost(typeof(MonitoringServiceProvider));
            host.AddServiceEndpoint(typeof(IMonitoringServer), binding, address);
            
            try
            {
                host.Open();
                Console.WriteLine("MonitoringServer is started.\nPress <enter> to stop ...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }
            finally
            {
                host.Close();
            }


        }
    }
}
