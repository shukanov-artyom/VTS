using System;
using System.Collections.Generic;
using VTS.Shared;

namespace VTSWeb.AnalysisCore.Recognition.Engines.Mappings
{
    public static class EngineModelDisplayNameProvider
    {
        private static IDictionary<EngineType, string> map = 
            new Dictionary<EngineType,string>();

        static EngineModelDisplayNameProvider()
        {
            map.Add(EngineType.DW10BTED4, "DW10 BTED4");
        }

        public static string Get(EngineType type)
        {
            if (!map.ContainsKey(type))
            {
                return type.ToString();
            }
            return map[type];
        }
    }
}
