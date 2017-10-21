using System;

namespace VTSWeb.VendorData.Extensions
{
    public class NoInfoForVinException : Exception
    {
        public NoInfoForVinException(string msg)
            : base (msg)
        {
            
        }
    }
}
