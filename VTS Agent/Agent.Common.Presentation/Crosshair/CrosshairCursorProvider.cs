using System;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Crosshair
{
    public abstract class CrosshairCursorProvider<T>
    {
        // Find a series point that is closest to an argument from 
        // chart coordinates.
        protected abstract double GetSeriesValue(Series series, T argument);

        protected abstract ControlCoordinates GetTopLeftCoordinates();

        protected abstract ControlCoordinates GetBottomRightCoordinates();
    }
}
