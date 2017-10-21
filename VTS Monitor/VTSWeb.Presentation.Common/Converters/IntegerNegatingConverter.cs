using System;
using System.Globalization;
using System.Windows.Data;

namespace VTSWeb.Presentation.Common.Converters
{
    public class IntegerNegatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            return Negate(value);
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            return Negate(value);
        }

        private object Negate(object value)
        {
            int val = Int32.Parse(value.ToString());
            if (val == 0)
            {
                return val;
            }
            return val * -1;
        }
    }
}
