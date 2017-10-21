using System;
using VTS.Shared;
using VTSWeb.AnalysisCore.Recognition.Engines.Mappings;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Presentation
{
    public class EngineTypeViewModel : ViewModelBase
    {
        private EngineType model;
        private string displayName;

        public EngineTypeViewModel(EngineType model)
        {
            this.model = model;
            displayName = EngineModelDisplayNameProvider.Get(model);
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public EngineType Model
        {
            get
            {
                return model;
            }
        }
    }
}
