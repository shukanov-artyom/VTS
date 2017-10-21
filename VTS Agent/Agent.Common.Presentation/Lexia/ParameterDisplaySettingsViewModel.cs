using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Agent.Common.Presentation.Controls;
using Agent.Common.Presentation.Data;

namespace Agent.Common.Presentation.Lexia
{
    public class ParameterDisplaySettingsViewModel : ViewModelBase
    {
        public event EventHandler ColorChanged;

        private static readonly ObservableCollection<Brush> availableColors =
            new ObservableCollection<Brush>();
        private ObservableCollection<ChartScaleViewModel> scales = 
            new ObservableCollection<ChartScaleViewModel>();
        private ChartScaleViewModel selectedScale;
        private readonly ViewModelBase vm;

        private Brush stroke;

        static ParameterDisplaySettingsViewModel()
        {
            InitializeBrushes();
        }

        public ParameterDisplaySettingsViewModel(ViewModelBase vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }
            this.vm = vm;
            SelectRandomColorFromAvailable();
            var autoScale = new ChartScaleViewModel(ChartScale.Auto);
            scales.Add(autoScale);
            selectedScale = autoScale;
        }

        public object GraphControl
        {
            get;
            set;
        }

        public PsaParameterDataViewModel ParameterDataViewModel
        {
            get
            {
                return ParamData as PsaParameterDataViewModel;
            }
        }

        public ObservableCollection<Brush> AvailableColors
        {
            get
            {
                return availableColors;
            }
        }

        public ObservableCollection<ChartScaleViewModel> Scales
        {
            get
            {
                return scales;
            }
        }

        public ChartScaleViewModel SelectedScale
        {
            get
            {
                return selectedScale;
            }
            set
            {
                selectedScale = value;
                OnPropertyChanged("SelectedScale");
            }
        }

        public Color StrokeColor
        {
            get
            {
                return ((SolidColorBrush) Stroke).Color;
            }
            set
            {
                Stroke = new SolidColorBrush(value);
                OnColorChanged();
            }
        }

        public Brush Stroke
        {
            get
            {
                return stroke;
            }
            set
            {
                stroke = value;
                OnColorChanged();
            }
        }

        public string Text
        {
            get;
            set;
        }

        public ViewModelBase ParamData
        {
            get
            {
                return vm;
            }
        }

        private static void InitializeBrushes()
        {
            availableColors.Add(new SolidColorBrush(Colors.Red));
            availableColors.Add(new SolidColorBrush(Colors.Blue));
            availableColors.Add(new SolidColorBrush(Colors.Green));
            availableColors.Add(new SolidColorBrush(Colors.Orange));
            availableColors.Add(new SolidColorBrush(Colors.DarkViolet));
            availableColors.Add(new SolidColorBrush(Colors.DeepPink));
            availableColors.Add(new SolidColorBrush(Colors.Black));
            availableColors.Add(new SolidColorBrush(Colors.Aqua));
            availableColors.Add(new SolidColorBrush(Colors.CadetBlue));
            availableColors.Add(new SolidColorBrush(Colors.Coral));
            availableColors.Add(new SolidColorBrush(Colors.Crimson));
            availableColors.Add(new SolidColorBrush(Colors.DarkBlue));
            availableColors.Add(new SolidColorBrush(Colors.DarkTurquoise));
            availableColors.Add(new SolidColorBrush(Colors.Goldenrod));
            availableColors.Add(new SolidColorBrush(Colors.Gray));
            availableColors.Add(new SolidColorBrush(Colors.Magenta));
            availableColors.Add(new SolidColorBrush(Colors.MediumOrchid));
            availableColors.Add(new SolidColorBrush(Colors.Navy));
            availableColors.Add(new SolidColorBrush(Colors.OrangeRed));
            availableColors.Add(new SolidColorBrush(Colors.Purple));
            availableColors.Add(new SolidColorBrush(Colors.SaddleBrown));
            availableColors.Add(new SolidColorBrush(Colors.Teal));
            availableColors.Add(new SolidColorBrush(Colors.SteelBlue));
        }

        private void SelectRandomColorFromAvailable()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int random = rnd.Next(0, availableColors.Count);
            Stroke = availableColors[random];
        }

        private void OnColorChanged()
        {
            OnPropertyChanged("StrokeColor");
            OnPropertyChanged("Stroke");
            EventHandler colorChanged = ColorChanged;
            if (colorChanged != null)
            {
                colorChanged.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
