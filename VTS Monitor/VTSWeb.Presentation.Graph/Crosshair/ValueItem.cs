using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Graph.Crosshair
{
    public class ValueItem : Control
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.
            Register("Text",
            typeof(string),
            typeof(ValueItem),
            new PropertyMetadata(string.Empty));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
    }
}
