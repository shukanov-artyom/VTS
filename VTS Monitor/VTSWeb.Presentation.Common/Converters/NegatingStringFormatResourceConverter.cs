using System;
using System.Globalization;
using System.Windows.Data;
using VTSWeb.Localization;

namespace VTSWeb.Presentation.Common.Converters
{
    public class NegatingStringFormatResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            string resourceKey = parameter.ToString();
            string format = CodeBehindStringResolver.Resolve(resourceKey);
            int negatedValue = ((int) value)*-1;
            return String.Format(format, negatedValue);
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
