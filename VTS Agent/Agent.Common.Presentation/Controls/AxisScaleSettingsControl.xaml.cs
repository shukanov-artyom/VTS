using System;
using System.Windows;
using System.Windows.Controls;
using Agent.Common.Presentation.Lexia;

namespace Agent.Common.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for AxisScaleSettingsControl.xaml
    /// </summary>
    public partial class AxisScaleSettingsControl : UserControl
    {
        public AxisScaleSettingsControl()
        {
            InitializeComponent();
        }

        private ChartScaleViewModel ViewModel
        {
            get
            {
                return DataContext as ChartScaleViewModel;
            }
        }

        private void TopDecrement(object sender, RoutedEventArgs e)
        {
            ViewModel.Top = DecrementValue(ViewModel.Top);
        }

        private void TopIncrement(object sender, RoutedEventArgs e)
        {
            ViewModel.Top = IncrementValue(ViewModel.Top);
        }

        private void BottomIncrement(object sender, RoutedEventArgs e)
        {
            ViewModel.Bottom = IncrementValue(ViewModel.Bottom);
        }

        private void BottomDecrement(object sender, RoutedEventArgs e)
        {
            ViewModel.Bottom = DecrementValue(ViewModel.Bottom);
        }

        private static double IncrementValue(double val)
        {
            return val + GetStep(val);
        }

        private static double DecrementValue(double val)
        {
            return val - GetStep(val);
        }

        private static double GetStep(double val)
        {
            double absval = Math.Abs(val);
            if (absval < 3)
            {
                return 0.3;
            }
            if (absval < 20)
            {
                return 3;
            }
            if (absval < 100)
            {
                return 10;
            }
            if (absval < 1000)
            {
                return 50;
            }
            return 150;
        }
    }

}
