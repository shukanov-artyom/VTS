using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Agent.Common.Presentation.BindingConverters
{
    public class MarkToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            double mark = (double)value;
            if (mark == 0 || double.IsNaN(mark))
            {
                return new SolidColorBrush(Colors.Gray);
            }
            if (mark >= 9.0)
            {
                return new SolidColorBrush(Colors.Green);
            }
            if (mark >= 5.0 && mark < 9.0)
            {
                return new SolidColorBrush(Colors.Orange);
            }
            if (mark < 5.0)
            {
                return new SolidColorBrush(Colors.Red);
            }
            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
