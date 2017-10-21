using System;
using System.Collections.ObjectModel;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.VehicleParametersChronology.Presentation
{
    public class VehicleChronologicalParameterItemViewModel : ViewModelBase
    {
        private readonly VehicleChronologicalParametersGroup group;
        private readonly VehicleChronologicalParameter parameter;
        private readonly ObservableCollection<VehicleChronologicalParameterItemViewModel> children = 
            new ObservableCollection<VehicleChronologicalParameterItemViewModel>();

        public VehicleChronologicalParameterItemViewModel(
            VehicleChronologicalParametersGroup group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            this.group = group;
            foreach (var v in group.Groups)
            {
                children.Add(new VehicleChronologicalParameterItemViewModel(v));
            }
            foreach (VehicleChronologicalParameter chronologicalParameter in group.Parameters)
            {
                children.Add(new VehicleChronologicalParameterItemViewModel(chronologicalParameter));
            }
        }

        public VehicleChronologicalParameterItemViewModel(
            VehicleChronologicalParameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }
            this.parameter = parameter;
        }

        public string DisplayText
        {
            get
            {
                return group == null ? 
                    CodeBehindStringResolver.Resolve(String.Format("AnalyticItemDisplayName{0}", parameter.Type.ToString())) :
                    CodeBehindStringResolver.Resolve(String.Format("AnalyticItemDisplayNameAnalyticModel{0}", group.GroupNameKey));
            }
        }

        public ObservableCollection<VehicleChronologicalParameterItemViewModel> Children
        {
            get
            {
                return children;
            }
        }

        public object Model
        {
            get
            {
                if (group == null)
                {
                    return parameter;
                }
                return group;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("DisplayText");
            base.ChangeLanguage();
        }
    }
}
