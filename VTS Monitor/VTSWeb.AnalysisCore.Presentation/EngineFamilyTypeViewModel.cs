using System;
using VTS.Shared;
using VTSWeb.AnalysisCore.Recognition.Engines.Mappings;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Presentation
{
    public class EngineFamilyTypeViewModel : ViewModelBase
    {
        private EngineFamilyType model;
        private string displayName;

        public EngineFamilyTypeViewModel(EngineFamilyType model)
        {
            this.model = model;
            displayName = EngineFamilyDisplayNameProvider.Get(model);
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public EngineFamilyType Model
        {
            get
            {
                return model;
            }
        }
    }
}
