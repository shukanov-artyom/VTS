using System;
using VTS.Shared;

namespace VTSWeb.AnalysisCore.Recognition.Engines
{
    public class EngineFamily
    {
        public EngineFamilyType Type
        {
            get;
            set;
        }

        public string Link
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }
    }
}
