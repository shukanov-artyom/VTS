using System;
using System.Windows.Controls;
using Agent.Common.Presentation.Controls;
using Agent.Workspace.ViewModels;

namespace Agent.Workspace.Views
{
    /// <summary>
    /// Interaction logic for ParametersSetView.xaml
    /// </summary>
    public partial class ParametersSetSettingsView : UserControl, IDataItemView
    {
        private readonly PsaParametersSetViewModel setViewModel;
        private readonly PsaParametersSetDetailsControl detailsControlInternalRef;

        private ParametersSetSettingsView()
        {
            InitializeComponent();
        }

        public ParametersSetSettingsView(PsaParametersSetViewModel viewModel)
            : this()
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            detailsControl.DataContext = setViewModel = viewModel;
            detailsControl.Content = detailsControlInternalRef = new PsaParametersSetDetailsControl(setViewModel);
            var tabbedView = new PsaParametersSetTabbedView(detailsControlInternalRef);
            detailsControlInternalRef.SetGraphControl(tabbedView.GraphControl);
            contentControlView.Content = tabbedView;
        }
    }
}
