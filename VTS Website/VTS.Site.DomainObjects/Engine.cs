using System;
using VTS.Shared;
using VTS.Site.DomainObjects;

namespace VTS.Site.AnalysisCore
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
