using System;
using Agent.Common.Presentation;
using Agent.Localization;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels.Maintenance
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
