using System;

namespace VTS.Site.DomainObjects.VendorData
{
    public class NoInfoForVinException : Exception
    {
        public NoInfoForVinException(string msg)
            : base(msg)
        {
        }
    }
}
