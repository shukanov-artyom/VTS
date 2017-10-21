using VTS.Shared;
using VTS.Site.AnalysisCore;

namespace VTS.Site.DomainObjects
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
