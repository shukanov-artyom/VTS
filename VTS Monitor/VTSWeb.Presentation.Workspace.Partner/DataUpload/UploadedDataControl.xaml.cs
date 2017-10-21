using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.Workspace.Psa;
using VTSWeb.Presentation.Graph;
using VTSWeb.Presentation.Graph.Checkboxes;
using VTSWeb.Presentation.Import;
using VTSWeb.Presentation.Psa.Interfaces;
using VTSWeb.Presentation.Workspace.Partner.DataUpload.Graphs;
using VTSWeb.Presentation.Workspace.Partner.DataUpload.ItemDetailPanels;

namespace VTSWeb.Presentation.Workspace.Partner.DataUpload
{
    public partial class UploadedDataControl : UserControl
    {
        private object currentSelectedItem = null;

        public UploadedDataControl()
        {
            InitializeComponent();
            ApplicationSizeKeeper.AppResized += OnGridSizeChanged;
            DialogWindowStatus.DialogWindowClosed += OnDialogClosed;
            OnGridSizeChanged(this, EventArgs.Empty);
        }

        private void OnGridSizeChanged(object sender, EventArgs e)
        {
            double hight = ApplicationSizeKeeper.Height - 240;
            DataTreeView.Height = hight;
            controlUpperLowerControls.Height = hight;

            double totalWidth = ApplicationSizeKeeper.Width;
            double something = 40;
            double leftWidth = totalWidth*2/5 - something;
            double rightWidth = totalWidth * 3 / 5 - something;

            DataTreeView.Width = leftWidth;
            controlUpperLowerControls.InnerContent.Width = rightWidth;
        }

        private TreeView DataTreeView
        {
            get
            {
                return (TreeView)mainTreeViewBorderControl.InnerContent;
            }
        }

        private ContentControl ContentControlLower
        {
            get
            {
                return ((DataGraphsControl)controlUpperLowerControls.
                    InnerContent).ContentControlLower;
            }
        }
        private ContentControl ContentControlUpper
        {
            get
            {
                return ((DataGraphsControl)controlUpperLowerControls.
                    InnerContent).ContentControlUpper;
            }
        }

        private void OnGridSelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            object obj = e.NewValue;
            ImportableVehicleViewModel veh =
                obj as ImportableVehicleViewModel;
            if (veh != null)
            {
                if (obj != currentSelectedItem)
                {
                    DeselectCurrentlySelected();
                    veh.IsSelected = true;
                    currentSelectedItem = veh;
                }
                VehicleItemDetailsControl vehDetails = 
                    new VehicleItemDetailsControl();

                UpdateFakeGraphControl(veh);

                vehDetails.DataContext = veh;
                ContentControlLower.Content = vehDetails;
                return;
            }
            ImportablePsaParameterDataViewModel parameterData =
                obj as ImportablePsaParameterDataViewModel;
            if (parameterData != null)
            {
                currentSelectedItem = parameterData;
                SingleParameterGraphControl graph = 
                    new SingleParameterGraphControl();
                if (parameterData.HasTimestamps)
                {
                    graph.DisplayGraph(parameterData.Model.Timestamps,
                        parameterData.Values, Colors.Blue);
                }
                else
                {
                    graph.DisplayGraph(parameterData.Values, Colors.Blue);
                }
                ContentControlLower.Content = graph;
                ContentControlUpper.Content = null;
                return;
                // set parameter description and some other info at top place
            }
            IPsaParametersSetViewModel set = obj as IPsaParametersSetViewModel;
            if (set != null)
            {
                currentSelectedItem = set;
                PsaParametersSetGraphSelectionControl selector = 
                    new PsaParametersSetGraphSelectionControl();
                selector.DataContext = set;
                ContentControlUpper.Content = selector;
                selector.InitializeCheckboxes();
                PsaParametersSetGraphControl multigraph = 
                    new PsaParametersSetGraphControl();
                ContentControlLower.Content = multigraph;
                selector.CheckBoxChecked -= OnSelectorCheckBoxChecked;
                selector.CheckBoxUnchecked -= OnSelectorCheckBoxUnChecked;
                selector.CheckBoxChecked += OnSelectorCheckBoxChecked;
                selector.CheckBoxUnchecked += OnSelectorCheckBoxUnChecked;
                return;
            }

            ContentControlUpper.Content = null;
            ContentControlLower.Content = null;
        }

        private void OnDialogClosed(object sender, EventArgs e)
        {
            RefreshEnableUi();
        }

        public void RefreshEnableUi()
        {
            IsEnabled = true;
            DataTreeView.IsEnabled = true;
            if (ContentControlUpper.Content != null)
            {
                ((UserControl)ContentControlUpper.Content).IsEnabled = true;
            }
            if (ContentControlLower.Content != null)
            {
                ((UserControl)ContentControlLower.Content).IsEnabled = true;
            }
            DataTreeView.IsEnabled = true;
            DataTreeView.IsEnabled = true;
        }

        private void UpdateFakeGraphControl(
            ImportableVehicleViewModel vehicleViewModel)
        {

        }

        private void DeselectCurrentlySelected()
        {
            if (currentSelectedItem is VehicleImportedDataViewModel)
            {
                ((VehicleImportedDataViewModel) 
                    currentSelectedItem).IsSelected = false;
            }
        }

        private void OnSelectorCheckBoxChecked(object sender, EventArgs e)
        {
            ParameterCheckBoxViewModel cbvm =
                sender as ParameterCheckBoxViewModel;
            if (cbvm == null)
            {
                throw new ArgumentException("Wrong sender!");
            }
            PsaParametersSetGraphControl controlGraph =
                ContentControlLower.Content as PsaParametersSetGraphControl;
            if (controlGraph == null)
            {
                throw new Exception("Wrong control is at place!");
            }
            controlGraph.AddGraph(cbvm.ParamData as IPsaParameterDataViewModel,
                cbvm.StrokeColor);
        }

        private void OnSelectorCheckBoxUnChecked(object sender, EventArgs e)
        {
            ParameterCheckBoxViewModel cbvm =
                sender as ParameterCheckBoxViewModel;
            if (cbvm == null)
            {
                throw new ArgumentException("Wrong sender!");
            }
            PsaParametersSetGraphControl controlGraph =
                ContentControlLower.Content as PsaParametersSetGraphControl;
            if (controlGraph == null)
            {
                throw new Exception("Wrong control is at place!");
            }
            controlGraph.RemoveGraph(cbvm.ParamData
                as IPsaParameterDataViewModel);
        }
    }
}
