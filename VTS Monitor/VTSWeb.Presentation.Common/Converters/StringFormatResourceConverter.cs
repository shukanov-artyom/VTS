using System;
using System.Globalization;
using System.Windows.Data;
using VTSWeb.Localization;

namespace VTSWeb.Presentation.Common.Converters
{
    public class StringFormatResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            string resourceKey = parameter.ToString();
            string format = CodeBehindStringResolver.Resolve(resourceKey);
            return String.Format(format, value);
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
