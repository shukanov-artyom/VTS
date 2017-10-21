using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Media;
using Agent.Localization;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Presentation.Data
{
    public class PsaParameterDataViewModel : ViewModelBase
    {
        private readonly PsaParameterData model;
        private readonly ObservableCollection<double> values = 
            new ObservableCollection<double>();
        private readonly ObservableCollection<int> timestamps =
            new ObservableCollection<int>();

        private SolidColorBrush color;

        private readonly UnitsViewModel unitsViewModel;

        private bool isSelected;

        public PsaParameterDataViewModel(PsaParameterData model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;

            foreach (string v in model.Values)
            {
                double val = double.NaN;
                double.TryParse(v, NumberStyles.Float,
                    CultureInfo.InvariantCulture, out val);
                values.Add(val);
            }

            if (model.HasTimestamps)
            {
                foreach (int ts in model.Timestamps)
                {
                    timestamps.Add(ts);
                }
            }
            unitsViewModel = new UnitsViewModel(model.Units);
        }

        public bool HasTimestamps
        {
            get
            {
                return model.HasTimestamps;
            }
        }

        public string Type
        {
            get
            {
                return CodeBehindStringResolver.Resolve(model.Type.ToString());
            }
        }

        public string OriginalName
        {
            get
            {
                return Model.OriginalName;
            }
        }

        public ObservableCollection<int> Timestamps
        {
            get
            {
                return timestamps;
            }
        }

        public ObservableCollection<double> Values
        {
            get
            {
                return values;
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public SolidColorBrush Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }

        public string Summary
        {
            get
            {
                return CodeBehindStringResolver.Resolve(model.Type.ToString());
            }
        }

        public PsaParameterData Model
        {
            get
            {
                return model;
            }
        }

        public UnitsViewModel MeasureUnits
        {
            get
            {
                return unitsViewModel;
            }
        }

        public string TextRepresentation
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (string value in Model.Values)
                {
                    builder.AppendFormat("{0};", value);
                }
                return builder.ToString();
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("Type");
            OnPropertyChanged("Summary");
            OnPropertyChanged("MeasureUnits");
            base.ChangeLanguage();
        }
    }
}
