using System;

namespace VTS.AnalysisCore.Common
{
    public class NoInfoForVinException : Exception
    {
        public NoInfoForVinException(string msg)
            : base(msg)
        {
        }
    }
}
