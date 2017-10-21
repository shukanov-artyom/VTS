using System;
using Agent.Common.Presentation.Controls;
using Agent.Localization;

namespace Agent.Common.Presentation.Lexia
{
    public class ChartScaleViewModel : ViewModelBase
    {
        private readonly ChartScale model;

        public ChartScaleViewModel(ChartScale model)
        {
            if (model == null)
            {
                throw new ArgumentException("model");
            }
            this.model = model;
        }

        public double Top
        {
            get
            {
                return model.Max;
            }
            set
            {
                model.Max = value;
                OnPropertyChanged("Top");
            }
        }

        public double Bottom
        {
            get
            {
                return model.Min;
            }
            set
            {
                model.Min = value;
                OnPropertyChanged("Bottom");
            }
        }

        public string Name
        {
            get
            {
                if (model.Equals(ChartScale.Auto))
                {
                    return CodeBehindStringResolver.Resolve("Autoscale");
                }
                if (model.Equals(ChartScale.Create))
                {
                    return "Create...";
                }
                return model.ToString();
            }
        }

        public ChartScale Model
        {
            get
            {
                return model;
            }
        }
    }
}
