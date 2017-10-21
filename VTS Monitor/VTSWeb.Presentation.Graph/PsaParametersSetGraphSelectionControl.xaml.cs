using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Graph.Checkboxes;
using VTSWeb.Presentation.Psa.Interfaces;

namespace VTSWeb.Presentation.Graph
{
    public partial class PsaParametersSetGraphSelectionControl : UserControl
    {
        private IList<Color> availableColors = new List<Color>();

        public event EventHandler CheckBoxChecked;
        public event EventHandler CheckBoxUnchecked;

        public PsaParametersSetGraphSelectionControl()
        {
            InitializeComponent();
            InitializeBrushes();
        }

        public void InitializeCheckboxes()
        {
            checkboxesStackPanel.Children.Clear();
            Queue<Color> colors = new Queue<Color>();
            foreach (Color c in availableColors)
            {
                colors.Enqueue(c);
            }
            foreach (IPsaParameterDataViewModel paramDataViewModel
                in ((IPsaParametersSetViewModel)DataContext).Params)
            {
                ParameterCheckBoxViewModel cbViewModel =
                    new ParameterCheckBoxViewModel(paramDataViewModel 
                        as ViewModelBase);
                Color current = colors.Dequeue();
                cbViewModel.StrokeColor = current;
                cbViewModel.Stroke = new SolidColorBrush(current);
                cbViewModel.Text = paramDataViewModel.Type.Name;
                ParameterCheckBoxControl cbControl =
                    new ParameterCheckBoxControl();
                cbControl.DataContext = cbViewModel;
                cbControl.Text = cbViewModel.Text;
                cbControl.LineColor = cbViewModel.StrokeColor;
                cbControl.IsChecked = false;
                cbControl.Checked += SomeCheckBoxChecked;
                cbControl.Unchecked += SomeCheckBoxUnChecked;
                cbControl.HorizontalAlignment = HorizontalAlignment.Left;
                checkboxesStackPanel.Children.Add(cbControl);
            }
        }

        private void InitializeBrushes()
        {
            availableColors.Add(Colors.Red);
            availableColors.Add(Colors.Blue);
            availableColors.Add(Colors.Green);
            availableColors.Add(Colors.Orange);
            availableColors.Add(Colors.Purple);
            availableColors.Add(Colors.Yellow);
            availableColors.Add(Colors.Black);
            availableColors.Add(Colors.Gray);
            availableColors.Add(Colors.Magenta);
            availableColors.Add(Colors.Purple);
            availableColors.Add(Colors.Red);
            availableColors.Add(Colors.Blue);
            availableColors.Add(Colors.Green);
            availableColors.Add(Colors.Red);
            availableColors.Add(Colors.Blue);
            availableColors.Add(Colors.Green);
            availableColors.Add(Colors.Orange);
            availableColors.Add(Colors.Purple);
            availableColors.Add(Colors.Yellow);
            availableColors.Add(Colors.Black);
            availableColors.Add(Colors.Gray);
            availableColors.Add(Colors.Magenta);
            availableColors.Add(Colors.Purple);
            availableColors.Add(Colors.Red);
            availableColors.Add(Colors.Blue);
            availableColors.Add(Colors.Green);
            availableColors.Add(Colors.Red);
            availableColors.Add(Colors.Blue);
            availableColors.Add(Colors.Green);
            availableColors.Add(Colors.Orange);
            availableColors.Add(Colors.Purple);
            availableColors.Add(Colors.Yellow);
            availableColors.Add(Colors.Black);
            availableColors.Add(Colors.Gray);
            availableColors.Add(Colors.Magenta);
            availableColors.Add(Colors.Purple);
            availableColors.Add(Colors.Red);
            availableColors.Add(Colors.Blue);
            availableColors.Add(Colors.Green);
        }

        private void SomeCheckBoxChecked(object sender, EventArgs e)
        {
            if (CheckBoxChecked != null)
            {
                CheckBoxChecked.Invoke(sender, e);
            }
        }

        private void SomeCheckBoxUnChecked(object sender, EventArgs e)
        {
            if (CheckBoxUnchecked != null)
            {
                CheckBoxUnchecked.Invoke(sender, e);
            }
        }

        private void ResetButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (ParameterCheckBoxControl cb in 
                checkboxesStackPanel.Children)
            {
                cb.IsChecked = false;
            }
        }
    }
}
