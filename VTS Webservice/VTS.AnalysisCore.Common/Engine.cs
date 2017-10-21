using System;
using VTS.Shared;

namespace VTS.AnalysisCore.Common
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
