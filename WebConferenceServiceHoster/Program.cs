using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WebConferenceService;

namespace WebConferenceServiceHoster
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Service));

            host.Opened += Host_Opened;

            var tcpBinding = new NetTcpBinding();
            tcpBinding.Security.Mode = SecurityMode.None;

            host.AddServiceEndpoint(typeof(IService), tcpBinding, "net.tcp://localhost:8888/WebConferenceService/");

            var behavior = new ServiceMetadataBehavior();

            host.Description.Behaviors.Add(behavior);
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "net.tcp://localhost:9999/WebConferenceService/mex/");

            host.Open();

            Console.WriteLine("Host is running on localhost at 8888 with mex at 9999... Press <Enter> key to stop");
            Console.ReadLine();
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Service is open at " + DateTime.Now.ToString());
        }
    }
}
