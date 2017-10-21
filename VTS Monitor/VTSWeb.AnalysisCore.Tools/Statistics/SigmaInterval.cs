using System;

namespace VTSWeb.AnalysisCore.Tools.Statistics
{
    internal class SigmaInterval
    {
        public SigmaInterval()
        {
            
        }

        public SigmaInterval(SigmaInterval origin)
        {
            Name = origin.Name;
            Bound1 = origin.Bound1;
            Bound2 = origin.Bound2;
            ValuesCount = origin.ValuesCount;
        }

        public string Name { get; set; }

        public double Bound1 { get; set; }

        public double Bound2 { get; set; }

        public long ValuesCount { get; set; }

        public bool Belongs(double value)
        {
            if (Bound1 == Bound2 && Bound1 == value)
            {
                return true;
            }
            return Bound1 >= value && Bound2 < value;
        }
    }
}
