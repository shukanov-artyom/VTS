using System.Collections.Generic;
using System.IO;
using Agent.Common.Data;

namespace VTS.Agent.Vendors
{
    public class DisplayNameToSoftwareMap
    {
        private Dictionary<string, SupportedSoftware> map = 
            new Dictionary<string, SupportedSoftware>();

        public DisplayNameToSoftwareMap()
        {
            // Multiecuscan
            string multiEcuScanName = Decode("t1GZC8CMS9UQhQQydggprQ=="); 
            //
            map.Add(multiEcuScanName, SupportedSoftware.MultiEcuScan);

        }

        public Dictionary<string, SupportedSoftware> Map
        {
            get
            {
                return map;
            }
        }

        private static string Decode(string s)
        {
            return Cipher.Decrypt(s, typeof (FileInfo).ToString());
        }
    }
}
