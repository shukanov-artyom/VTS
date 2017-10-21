using System;
using System.Collections.ObjectModel;
using Agent.Common.Presentation.Lexia;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Controls
{
    public class ScalesSetViewModel : ViewModelBase
    {
        private readonly XYDiagram2D model;
        private ChartScaleViewModel mainScale;
        private readonly ObservableCollection<ChartScaleViewModel> secondaryScales = 
            new ObservableCollection<ChartScaleViewModel>();

        public ScalesSetViewModel(XYDiagram2D model)
        {
            this.model = model;
            LoadScales();
        }

        public ChartScaleViewModel MainScale
        {
            get
            {
                return mainScale;
            }
            set
            {
                mainScale = value;
                OnPropertyChanged("MainScale");
            }
        }

        public ObservableCollection<ChartScaleViewModel> Scales
        {
            get
            {
               return secondaryScales;
            }
        }

        public void Refresh()
        {
            secondaryScales.Clear();
            LoadScales();
        }

        private void LoadScales()
        {
            foreach (SecondaryAxisY2D axis in model.SecondaryAxesY)
            {
                ChartScale scale = new ChartScale(axis);
                Scales.Add(new ChartScaleViewModel(scale));
                /*if (!(scale.Max == 0 && scale.Min == 0))
                {
                    Scales.Add(scale);
                }*/
            }
            ChartScale scaleMain = new ChartScale(model.AxisY);
            if (!(scaleMain.Max == scaleMain.Min && scaleMain.Max == 0))
            {
                MainScale = new ChartScaleViewModel(scaleMain);
            }
        }

        public void Redundant()
        {
            MainScale.ToString();
        }
    }
}
