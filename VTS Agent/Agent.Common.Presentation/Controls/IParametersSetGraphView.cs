using System;
using System.Windows.Media;
using Agent.Common.Presentation.Data;
using Agent.Common.Presentation.Lexia;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Controls
{
    public interface IParametersSetGraphView
    {
        SeriesCollection SeriesCollection
        {
            get;
        }

        Series CreateSeries(PsaParameterDataViewModel vm, Color color, ChartScaleViewModel scale);

        void AddSeries(Series series);

        Series FindSeries(PsaParameterDataViewModel vm);

        void RemoveSeries(Series s);
    }
}
