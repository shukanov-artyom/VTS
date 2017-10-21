using System;
using System.Windows;
using System.Windows.Controls;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Statistics.Generation;
using VTSWeb.AnalysisCore.Statistics.Generation.PagedRetrievers;
using VTSWeb.AnalysisCore.VehicleParametersChronology;
using VTSWeb.AnalysisCore.VehicleParametersChronology.Presentation;
using VTSWeb.Chrono.Presentation;

using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.Presentation.Common.Progress;
using VTSWeb.Presentation.Common.Vehicles;
using AnalyticStatistics = VTSWeb.AnalysisCore.Statistics.AnalyticStatistics;

namespace VTSWeb.Presentation.Workspace.Common.Analytic
{
    public partial class ManualDataAnalysisControl : NavigatablePage
    {
        private Vehicle selectedVehicle;
        private StatisticsGenerationEngine engine;

        public ManualDataAnalysisControl()
        {
            InitializeComponent();
            InitializeHeader("ManualDataAnalysis");
            ((VehicleSelectionControl)controlVehicleSelection.InnerContent).
                VehicleSelected += OnVehicleSelected;
            ApplicationSizeKeeper.AppResized += OnAppResized;
            Refresh();
        }

        private void Refresh()
        {
            OnAppResized(this, EventArgs.Empty);
        }

        private ContentControl DataTreeControl
        {
            get
            {
                return ((ContentControl) contentControlChronologicalDataTree
                    .InnerContent);
            }
        }

        private ContentControl ItemDetailsControl
        {
            get
            {
                return (ContentControl)contentControlItemPresentation.InnerContent;
            }
        }

        private void OnAppResized(object sender, EventArgs e)
        {
            DataTreeControl.Height = ApplicationSizeKeeper.Height - 250;
            DataTreeControl.Width = ApplicationSizeKeeper.Width * 2 / 5 - 20;
            ItemDetailsControl.Height = ApplicationSizeKeeper.Height - 250;
            ItemDetailsControl.Width = ApplicationSizeKeeper.Width * 3 / 5 - 100;
        }

        private void OnVehicleSelected(object sender, 
            SelectionChangedEventArgs e)
        {
            ClearItemPresentation();
            if (e.AddedItems.Count == 0)
            {
                ClearView();
                return;
            }
            selectedVehicle = 
                ((VehicleViewModel)e.AddedItems[0]).Model as Vehicle;
            LoadAndDisplayStatistics();
        }

        private void LoadAndDisplayStatistics()
        {
            if (selectedVehicle == null)
            {
                throw new Exception("Vehicle shoudbe already selected by this moment.");
            }
            engine = new StatisticsGenerationEngine(
                StatisticsGenerationPercentageUpdatedCallback,
                StatisticsGeneratedCallback,
                OnError,
                new VehicleDatasetsPagedRetriever(selectedVehicle.Id));
            DataTreeControl.Content = new CircularProgressBar();
            ((CircularProgressBar) DataTreeControl.Content).Margin = 
                new Thickness(80, 0, 0, 0);
            ((CircularProgressBar) DataTreeControl.Content).Progress = 0;
            engine.StartGeneration();
        }

        private void StatisticsGenerationPercentageUpdatedCallback(int percent)
        {
            ((CircularProgressBar)DataTreeControl.Content).Progress = percent;
        }

        private void StatisticsGeneratedCallback()
        {
            AnalyticStatistics vehicleStatistics = engine.Result;
            engine = null;
            VehicleParametersChronology chronology =
                GenerateVehicleParametersChronology(vehicleStatistics);
            VehicleParametersChronologyTreeControl treeControl =
                new VehicleParametersChronologyTreeControl(
                    new VehicleParametersChronologyViewModel(chronology), 
                    contentControlItemPresentation);
            controlVehicleSelection.SetWaitingMode(false);
            SetTreeControl(treeControl);
        }

        private void SetTreeControl(VehicleParametersChronologyTreeControl treeControl)
        {
            if (contentControlChronologicalDataTree != null)
            {
                var oldTreeControl = contentControlChronologicalDataTree.
                    InnerContent as VehicleParametersChronologyTreeControl;
                if (oldTreeControl != null)
                {
                    oldTreeControl.SelectedTreeItemChanged -= OnSelectedTreeItemChanged;
                }
            }
            treeControl.SelectedTreeItemChanged += OnSelectedTreeItemChanged;
            contentControlChronologicalDataTree.
                SetContentIfContentControl(treeControl);
        }

        private void OnSelectedTreeItemChanged(object sender, 
            RoutedPropertyChangedEventArgs<object> e)
        {
            object item = ((VehicleChronologicalParameterItemViewModel)e.NewValue).Model;
            if (item is VehicleChronologicalParameter)
            {
                UserControl control =
                    new VehicleChronologicalParameterGraphControl(
                        new VehicleChronologicalParameterViewModel(
                            item as VehicleChronologicalParameter));
                ItemDetailsControl.Content = control;
            }
        }

        private VehicleParametersChronology GenerateVehicleParametersChronology(
            AnalyticStatistics statistics)
        {
            VehicleParametersChronologyFactory factory = 
                new VehicleParametersChronologyFactory(statistics);
            return factory.Create();
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Show();
        }

        private void SetPresentationWaitingMode(bool wait)
        {
            contentControlItemPresentation.SetWaitingMode(wait);
        }

        private void ShowItemPresentation(UserControl presentation)
        {
            SetPresentationWaitingMode(false);
            ItemDetailsControl.Content = presentation;
        }

        private void ClearItemPresentation()
        {
            ShowItemPresentation(null);
        }

        private void ClearView()
        {
            ChronologicalDataPresentation cdp = DataTreeControl.Content as
                ChronologicalDataPresentation;
            if (cdp == null)
            {
                throw new Exception(
                    "Wrong control at place of chrono data representation");
            }
            cdp.Clear();
        }
    }
}
