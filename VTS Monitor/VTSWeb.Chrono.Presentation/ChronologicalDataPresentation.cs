using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Chrono.Presentation
{
    public abstract class ChronologicalDataPresentation : UserControl
    {
        public event RoutedPropertyChangedEventHandler<
            object> SelectionChanged;

        protected void OnSelectionChanged(
            object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged.Invoke(sender, e);
            }
        }

        public abstract void Clear();
    }
}
