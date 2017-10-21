using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTSWeb.AnalysisCore.Statistics.Presentation.MeasureUnits;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.VehicleParametersChronology.Presentation
{
    public class VehicleChronologicalParameterViewModel : ViewModelBase
    {
        private VehicleChronologicalParameter model;
        private readonly ObservableCollection<KeyValuePair<DateTime, double>> dataForGraph = 
            new ObservableCollection<KeyValuePair<DateTime, double>>();

        private readonly UnitsViewModel units;

        public VehicleChronologicalParameterViewModel(VehicleChronologicalParameter model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            foreach (var pair in model.Values)
            {
                DataForGraph.Add(pair);
            }
            units = new UnitsViewModel(
                AnalyticRuleStatisticsMeasureUnitsResolver.Resolve(model.Type));
        }

        public ObservableCollection<KeyValuePair<DateTime, double>> DataForGraph
        {
            get
            {
                return dataForGraph;
            }
        }

        public UnitsViewModel Units
        {
            get
            {
                return units;
            }
        }
    }
}
