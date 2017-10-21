using System;
using System.Collections.Generic;
using System.Windows.Media;
using DevExpress.Xpf.Charts;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Psa.Interfaces;

namespace VTSWeb.Presentation.Graph
{
    public partial class PsaParametersSetGraphControl : MultipleGraphControl
    {
        private Dictionary<IPsaParameterDataViewModel, Color> current = 
            new Dictionary<IPsaParameterDataViewModel,Color>();

        public PsaParametersSetGraphControl()
        {
            InitializeComponent();
        }

        public bool Zoomed
        {
            get; 
            set;
        }

        public void AddGraph(IPsaParameterDataViewModel vm, Color strokeColor)
        {
            current.Add(vm, strokeColor);
            LineSeries2D series = new LineSeries2D();
            series.DataContext = vm;
            series.Brush = new SolidColorBrush(strokeColor);
            series.ValueScaleType = ScaleType.Numerical;
            series.ArgumentScaleType = ScaleType.Numerical;
            series.MarkerVisible = false;
            series.ShowInLegend = false;
            series.Label = new SeriesLabel();
            series.LabelsVisibility = false;

            for (int i = 0; i < vm.Values.Count; i++)
            {
                SeriesPoint pt;
                if (!vm.HasTimestamps)
                {
                    pt = new SeriesPoint(i, vm.Values[i]);
                }
                else
                {
                    pt = new SeriesPoint(vm.Model.Timestamps[i], vm.Values[i]);
                }
                series.Points.Add(pt);
            }
            if (graphDiagram.Series.Count > 0)
            {
                AxisY2D compatibleAxis =
                    FindCompatibleAxis(graphDiagram, series);

                if (compatibleAxis == null)
                {
                    SecondaryAxisY2D newAxis = GenerateNewAxis(series);
                    newAxis.Brush = new SolidColorBrush(strokeColor);
                    graphDiagram.SecondaryAxesY.Add(newAxis);
                    XYDiagram2D.SetSeriesAxisY(series, newAxis);
                }
                else
                {
                    if (compatibleAxis is SecondaryAxisY2D)
                    {
                        UpdateAxisByNewSeries(
                            (SecondaryAxisY2D)compatibleAxis, series);
                        XYDiagram2D.SetSeriesAxisY(series,
                        (SecondaryAxisY2D)compatibleAxis);
                    }
                }
            }
            graphDiagram.Series.Add(series);
        }

        public void RemoveGraph(IPsaParameterDataViewModel vm)
        {
            current.Remove(vm);
            Series toDelete = null;
            foreach (Series s in graphDiagram.Series)
            {
                IPsaParameterDataViewModel param =
                        s.DataContext as IPsaParameterDataViewModel;
                if (param != null)
                {
                    if (param.Type == vm.Type)
                    {
                        toDelete = s;
                        break;
                    }
                }
            }
            // determine axes to delete
            AxisY2D axsToDelete = XYDiagram2D.
                GetSeriesAxisY((XYSeries)toDelete);
            bool axsUsedSomewhereElse = false;
            foreach (XYSeries s in graphDiagram.Series)
            {
                if (s != null && s == toDelete)
                {
                    continue;
                }
                AxisY2D axs = XYDiagram2D.GetSeriesAxisY(s);
                if (axs != null && axs == axsToDelete)
                {
                    axsUsedSomewhereElse = true;
                }
            }
            graphDiagram.Series.Remove(toDelete);
            SecondaryAxisY2D axsToDeleteCast =
                axsToDelete as SecondaryAxisY2D;
            if (!axsUsedSomewhereElse && axsToDeleteCast != null)
            {
                graphDiagram.SecondaryAxesY.
                    Remove(axsToDeleteCast);
            }
        }

        private void OnGraphClicked(object sender, EventArgs e)
        {
            if (Zoomed)
            {
                return;
            }
            GraphZoomWindow w = new GraphZoomWindow();
            PsaParametersSetGraphControl control = 
                new PsaParametersSetGraphControl();
            control.Zoomed = true;
            foreach (KeyValuePair<IPsaParameterDataViewModel, 
                Color> kvp in current)
            {
                control.AddGraph(kvp.Key, kvp.Value);
            }
            w.PutControl(control);
            w.Closed += DialogWindowStatus.OnDialogClosed;
            w.Show();
        }
    }
}
