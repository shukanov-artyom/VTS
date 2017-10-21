using System;
using System.ServiceModel;
using VTSWebService;

namespace Test.SelfHoster
{
    public static class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(VtsWebService));
            host.Open();
            Console.Read();
        }
    }
}
