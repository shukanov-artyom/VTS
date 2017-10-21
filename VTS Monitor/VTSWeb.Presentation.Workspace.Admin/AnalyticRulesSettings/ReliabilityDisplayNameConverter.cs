using System;
using System.Globalization;
using System.Windows.Data;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.Localization;

namespace VTSWeb.Presentation.Workspace.Admin.AnalyticRulesSettings
{
    public class ReliabilityDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            AnalyticItemSettingsReliability reliability =
                (AnalyticItemSettingsReliability)value;
            string key = String.Format("Reliability{0}DisplayName", reliability);
            return CodeBehindStringResolver.Resolve(key);
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
