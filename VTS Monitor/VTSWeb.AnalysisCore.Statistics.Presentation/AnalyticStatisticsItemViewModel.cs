using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTSWeb.AnalysisCore.Presentation;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Statistics.Presentation
{
    public class AnalyticStatisticsItemViewModel : ViewModelBase
    {
        private AnalyticStatisticsItem model;
        private EngineFamilyTypeViewModel familyType;
        private EngineTypeViewModel engineType;
        private AnalyticStatisticsItemTypeViewModel itemType;
        private ObservableCollection<AnalyticStatisticsSourceVinViewModel> values =
            new ObservableCollection<AnalyticStatisticsSourceVinViewModel>();

        public AnalyticStatisticsItemViewModel(AnalyticStatisticsItem model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            familyType = 
                new EngineFamilyTypeViewModel(model.TargetEngineFamilyType);
            engineType = 
                new EngineTypeViewModel(model.TargetEngineType);
            itemType = new AnalyticStatisticsItemTypeViewModel(model.Type);
            foreach (AnalyticStatisticsValue val in model.Values)
            {
                if (values.Any(v => v.Vin == val.SourceVin))
                {
                    values.First(v => v.Vin == val.SourceVin).
                        AddId(val.SourcePsaParametersSetId);
                }
                else
                {
                    values.Add(new AnalyticStatisticsSourceVinViewModel(
                        val.SourceVin, val.SourcePsaParametersSetId));
                }
            }
        }

        public ObservableCollection<AnalyticStatisticsSourceVinViewModel> Values
        {
            get
            {
                return values;
            }
        }

        public string Type
        {
            get
            {
                return itemType.DisplayName;
            }
        }

        public string EngineFamily
        {
            get
            {
                return familyType.DisplayName;
            }
        }

        public string EngineType
        {
            get
            {
                return engineType.DisplayName;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("Type");
            base.ChangeLanguage();
        }

        public AnalyticStatisticsItem Model
        {
            get
            {
                return model;
            }
        }
    }
}
