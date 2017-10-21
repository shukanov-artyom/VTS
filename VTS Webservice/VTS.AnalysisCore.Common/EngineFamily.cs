using System;
using VTS.Shared;
using VTSWebService.AnalysisCore.Enums;

namespace VTS.AnalysisCore.Common
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
