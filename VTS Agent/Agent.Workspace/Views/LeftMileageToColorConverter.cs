using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Agent.Workspace.Views
{
    public class LeftMileageToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            int mileageLeft = (int)value;
            if (mileageLeft > 1000 && mileageLeft <= 3000)
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else if (mileageLeft <= 1000)
            {
                return new SolidColorBrush(Colors.Red);
            }
            return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
