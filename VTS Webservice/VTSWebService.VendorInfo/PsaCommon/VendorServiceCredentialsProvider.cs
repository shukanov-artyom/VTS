using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWebService.VendorInfo.PsaCommon
{
    internal class VendorServiceCredentialsProvider
    {
        private IDictionary<string, string> citroenCredentials =
            new Dictionary<string, string>();
        private IDictionary<string, string> peugeotCredentials =
            new Dictionary<string, string>();

        public VendorServiceCredentialsProvider()
        {
            peugeotCredentials.Add("AP69147668", "toriyama");
            peugeotCredentials.Add("AP81168729", "a1s2d3");
            peugeotCredentials.Add("AP13369327", "zaxscd");

            citroenCredentials.Add("AC47473733", "toriyama");
            citroenCredentials.Add("AC28026280", "q1w2e3");
            citroenCredentials.Add("AC17799791", "zqxwce");
        }

        public KeyValuePair<string, string> GetCitroenCredentials()
        {
            int count = citroenCredentials.Count;
            Random rnd = new Random(DateTime.Now.Second);
            int num = rnd.Next(0, count - 1);
            return citroenCredentials.ToList()[num];
        }

        public KeyValuePair<string, string> GetPeugeotCredentials()
        {
            int count = peugeotCredentials.Count;
            Random rnd = new Random(DateTime.Now.Second);
            int num = rnd.Next(0, count - 1);
            return peugeotCredentials.ToList()[num];
        }
    }
}
