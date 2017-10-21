using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VTSWeb.Presentation.Graph;
using VTSWeb.Presentation.Graph.Checkboxes;
using VTSWeb.Presentation.Psa;
using VTSWeb.Presentation.Psa.Interfaces;

namespace VTSWeb.Presentation.Workspace.Common.Data
{
    public class SavedDataPresenter
    {
        private DataGraphsControl graphs;

        public SavedDataPresenter(VehicleDatasetsTreeControl tree, 
            DataGraphsControl graphs)
        {
            tree.SelectionChanged += OnSelectedDataItemChanged;
            this.graphs = graphs;
        }

        private ContentControl ContentControlLower
        {
            get
            {
                return graphs.ContentControlLower;
            }
        }
        private ContentControl ContentControlUpper
        {
            get
            {
                return graphs.ContentControlUpper;
            }
        }

        private void OnSelectedDataItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            object newObject = e.NewValue;
            PsaParametersSetViewModel set =
                newObject as PsaParametersSetViewModel;
            if (set != null)
            {
                if (set != null)
                {
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
            }
            PsaParameterDataViewModel parameter =
                newObject as PsaParameterDataViewModel;
            if (parameter != null)
            {
                SingleParameterGraphControl graph =
                    new SingleParameterGraphControl();
                if (parameter.HasTimestamps)
                {
                    graph.DisplayGraph(parameter.Model.Timestamps,
                        parameter.Values, Colors.Blue);
                }
                else
                {
                    graph.DisplayGraph(parameter.Values, Colors.Blue);
                }
                ContentControlLower.Content = graph;
                PsaParameterDataPropertiesControl propsControl =
                    new PsaParameterDataPropertiesControl();
                propsControl.DataContext = parameter;
                ContentControlUpper.Content = propsControl;
                return;
            }
            ContentControlLower.Content = null;
            ContentControlUpper.Content = null;
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
