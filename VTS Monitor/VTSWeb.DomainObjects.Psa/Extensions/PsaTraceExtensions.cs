using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;

namespace VTSWeb.DomainObjects.Psa.Extensions
{
    public static class PsaTraceExtensions
    {
        public static IList<string> GetVins(this IEnumerable<PsaTrace> traces)
        {
            IList<string> vins = new List<string>();
            foreach (PsaTrace t in traces)
            {
                if (!vins.Contains(t.Vin))
                {
                    vins.Add(t.Vin);
                }
            }
            return vins;
        }

        public static int GetMaxMileage(this IEnumerable<PsaTrace> traces)
        {
            int mileage = 0;
            foreach (PsaTrace t in traces)
            {
                if (t.Mileage > mileage)
                {
                    mileage = t.Mileage;
                }
            }
            return mileage;
        }

        public static bool AreAllVinsEqual(this IEnumerable<PsaTrace> traces)
        {
            IList<string> vins = GetVins(traces);
            return vins.Count == 1;
        }
    }
}
