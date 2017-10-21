using System;
using System.Collections.Generic;
using VTS.Shared;

namespace VTSWeb.AnalysisCore.Recognition.Engines.Mappings
{
    public static class EngineFamilyDisplayNameProvider
    {
        private static IDictionary<EngineFamilyType, string> mapping = 
            new Dictionary<EngineFamilyType,string>();

        static EngineFamilyDisplayNameProvider()
        {
            mapping.Add(EngineFamilyType.Douvrin ,"Douvrin");
            mapping.Add(EngineFamilyType.X, "X");
            mapping.Add(EngineFamilyType.TU, "TU");
            mapping.Add(EngineFamilyType.EC, "EC");
            mapping.Add(EngineFamilyType.Prince, "Prince");
            mapping.Add(EngineFamilyType.TUD, "TUD");
            mapping.Add(EngineFamilyType.XU, "XU");
            mapping.Add(EngineFamilyType.XUD, "XUD");
            mapping.Add(EngineFamilyType.EWDW, @"EW\DW");
            mapping.Add(EngineFamilyType.DK, "DK");
            mapping.Add(EngineFamilyType.DV, "DV");
            mapping.Add(EngineFamilyType.PRV, "PRV");
            mapping.Add(EngineFamilyType.ES, "ES");
            mapping.Add(EngineFamilyType.XB, "XB");
            mapping.Add(EngineFamilyType.XC, "XC");
            mapping.Add(EngineFamilyType.XD, "XD");
            mapping.Add(EngineFamilyType.XM, "XM");
            mapping.Add(EngineFamilyType.XN, "XN");
            mapping.Add(EngineFamilyType.EB, "EB");
        }

        public static string Get(EngineFamilyType type)
        {
            return mapping[type];
        }
    }
}
