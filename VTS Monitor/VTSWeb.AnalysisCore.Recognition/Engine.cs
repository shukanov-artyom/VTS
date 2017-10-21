using System;
using VTS.Shared;
using VTSWeb.AnalysisCore.Recognition.Engines;

namespace VTSWeb.AnalysisCore.Recognition
{
    public class Engine
    {
        public EngineType Type
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public EngineFamily Family
        {
            get;
            set;
        }

        public FuelType FuelType
        {
            get;
            set;
        }

        public InjectionType InjectionType
        {
            get;
            set;
        }
    }
}
