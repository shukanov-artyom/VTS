using System;
using System.Collections.Generic;
using System.Windows;
using Agent.Common.Instance;
using Agent.Common.Presentation.Controls.Cascade;
using Agent.Common.Presentation.Data;
using Agent.Common.Presentation.Lexia;

namespace Agent.Common.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for ParametersSetDetailsControl.xaml
    /// </summary>
    public partial class PsaParametersSetDetailsControl : IParametersSetSettingsSource
    {
        public event EventHandler GraphAdded;
        public event EventHandler GraphRemoved;
        public event EventHandler GraphColorChanged;

        private PsaParametersSetGraphControl graph;

        public PsaParametersSetDetailsControl()
        {
            InitializeComponent();
        }

        public PsaParametersSetDetailsControl(IPsaParametersSetViewModel vm)
            : this()
        {
            InitializeCheckBoxes(vm);
        }

        public void SetGraphControl(PsaParametersSetGraphControl graph)
        {
            this.graph = graph;
        }

        private void InitializeCheckBoxes(IPsaParametersSetViewModel vm)
        {
            checkboxesStackPanel.Children.Clear();
            DataContext = vm;
            foreach (PsaParameterDataViewModel paramDataViewModel in vm.Parameters)
            {
                ParameterDisplaySettingsViewModel cbViewModel =
                    new ParameterDisplaySettingsViewModel(paramDataViewModel);
                cbViewModel.GraphControl = graph;
                cbViewModel.Text = paramDataViewModel.Type;
                ParameterDisplaySettingsControl cbControl =
                    new ParameterDisplaySettingsControl();
                cbControl.DataContext = cbViewModel;
                cbControl.Text = cbViewModel.Text;
                cbControl.LineColor = cbViewModel.StrokeColor;
                cbControl.IsChecked = false;
                cbControl.Checked += SomeCheckBoxChecked;
                cbControl.Unchecked += SomeCheckBoxUnChecked;
                cbViewModel.ColorChanged += SomeCheckBoxColorChanged;
                cbControl.HorizontalAlignment = HorizontalAlignment.Left;
                checkboxesStackPanel.Children.Add(cbControl);
            }
        }

        private void SomeCheckBoxChecked(object sender, EventArgs e)
        {
            if (GraphAdded != null)
            {
                GraphAdded.Invoke(sender, e);
            }
        }

        private void SomeCheckBoxUnChecked(object sender, EventArgs e)
        {
            if (GraphRemoved != null)
            {
                GraphRemoved.Invoke(sender, e);
            }
        }

        private void SomeCheckBoxColorChanged(object sender, EventArgs e)
        {
            EventHandler colorChanged = GraphColorChanged;
            if (colorChanged != null)
            {
                colorChanged.Invoke(sender, e);
            }
        }

        private void ResetButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (ParameterDisplaySettingsControl cb in 
                checkboxesStackPanel.Children)
            {
                cb.IsChecked = false;
            }
        }

        private void CascadeButtonClick(object sender, RoutedEventArgs e)
        {
            CascadeWindow wnd = new CascadeWindow();
            wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
            wnd.DataContext = new CascadeViewModel(GetSelectedPsaParameterDatas());
            wnd.ShowDialog();
        }

        private IEnumerable<PsaParameterDataViewModel> GetSelectedPsaParameterDatas()
        {
            foreach (ParameterDisplaySettingsControl cb in checkboxesStackPanel.Children)
            {
                if (cb.IsChecked)
                {
                    var displaySettingsViewModel = cb.DataContext as ParameterDisplaySettingsViewModel;
                    if (displaySettingsViewModel != null)
                    {
                        yield return displaySettingsViewModel.ParameterDataViewModel;
                    }
                }
            }
        }
    }
}
