using System;
using VTS.Shared.DomainObjects;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.VehicleEvents.Presentation
{
    public class VehicleEventTypeViewModel : ViewModelBase
    {
        private const string Format = "VehicleEventTypeDisplayName{0}";
        private VehicleEventType model;

        public VehicleEventTypeViewModel(VehicleEventType model)
        {
            this.model = model;
        }

        public string DisplayName
        {
            get
            {
                return CodeBehindStringResolver.Resolve(
                    String.Format(Format, model));
            }
        }

        public VehicleEventType Model
        {
            get
            {
                return model;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("DisplayName");
            base.ChangeLanguage();
        }
    }
}
