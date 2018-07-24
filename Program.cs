using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ApetitWCFService
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			string baseAddress = "http://" + Environment.MachineName + ":8000/Service1";
            ServiceHost host = new ServiceHost(typeof(Service1), new Uri(baseAddress));
            host.AddServiceEndpoint(typeof(IService1), new WebHttpBinding(), "").Behaviors.Add(new WebHttpBehavior());
            host.Open();
            Console.WriteLine("Press Enter to close host");
            Console.ReadLine();
            host.Close();
        }
    }
}
