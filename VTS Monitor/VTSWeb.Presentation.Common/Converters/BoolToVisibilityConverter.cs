using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VTSWeb.Presentation.Common.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            if ((string)parameter == "Direct" || parameter == null)
            {
                if ((bool)value)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            if ((string)parameter == "Inverse")
            {
                if (!(bool)value)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            throw new ArgumentException("Incorrect parameter value");
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            if ((string)parameter == "Direct" || parameter == null)
            {
                if ((Visibility)value == Visibility.Collapsed)
                {
                    return false;
                }
                return true;
            }
            if ((string)parameter == "Inverse")
            {
                if ((Visibility)value == Visibility.Visible)
                {
                    return false;
                }
                return true;
            }
            throw new ArgumentException("Incorrect parameter value");
        }
    }
}
