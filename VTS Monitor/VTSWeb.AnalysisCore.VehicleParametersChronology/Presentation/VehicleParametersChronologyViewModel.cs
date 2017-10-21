using System;
using System.Collections.ObjectModel;

namespace VTSWeb.AnalysisCore.VehicleParametersChronology.Presentation
{
    public class VehicleParametersChronologyViewModel
    {
        private VehicleParametersChronology model;
        ObservableCollection<VehicleChronologicalParameterItemViewModel> groups = 
            new ObservableCollection<VehicleChronologicalParameterItemViewModel>();

        public VehicleParametersChronologyViewModel(
            VehicleParametersChronology model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            foreach (VehicleChronologicalParametersGroup groupModel in model.Groups)
            {
                groups.Add(new VehicleChronologicalParameterItemViewModel(
                    groupModel));
            }
        }

        public ObservableCollection<VehicleChronologicalParameterItemViewModel> Groups
        {
            get
            {
                return groups;
            }
        }
    }
}
