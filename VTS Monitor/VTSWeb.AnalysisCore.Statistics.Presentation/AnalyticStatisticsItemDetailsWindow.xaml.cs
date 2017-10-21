using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.AnalysisCore.Tools.Statistics;
using VTSWeb.Localization;

namespace VTSWeb.AnalysisCore.Statistics.Presentation
{
    public partial class AnalyticStatisticsItemDetailsWindow : ChildWindow
    {
        private AnalyticStatisticsItemViewModel item;
        private float discretion;
        private double expectation;
        private double sigma;

        public AnalyticStatisticsItemDetailsWindow()
        {
            InitializeComponent();
            controlDisplaySettings.buttonApply.Click += ButtonApplyOnClick;
        }

        public AnalyticStatisticsItemDetailsWindow(
            AnalyticStatisticsItemViewModel item)
            : this()
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            this.item = item;

            sigma = Math.Round(Sigma.Get(item.Model.GetDoubleValues().ToList()), 2);
            expectation = Math.Round(item.Model.GetDoubleValues().Average(), 2);

            controlProperties.textBlockElementType.Text = item.Type;
            controlProperties.textBlockEngineFamily.Text = item.EngineFamily;
            controlProperties.textBlockEngine.Text = item.EngineType;
            controlProperties.textBlockExpectation.Text = 
                expectation.ToString(CultureInfo.InvariantCulture);
            controlProperties.textBlockSigma.Text =
                sigma.ToString(CultureInfo.InvariantCulture);
            discretion = float.Parse(
                controlDisplaySettings.textBlockDiscretionValue.Text,
                NumberStyles.Float, CultureInfo.InvariantCulture);
            IDictionary<string, long> distribution = GetDistribution();
            controlGraphicalRepresentation.DisplayDistribution(distribution);
        }

        private void OnOkClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonApplyOnClick(
            object sender, RoutedEventArgs routedEventArgs)
        {
            if (!float.TryParse(
                controlDisplaySettings.textBlockDiscretionValue.Text,
                NumberStyles.Float, CultureInfo.InvariantCulture,
                out discretion))
            {
                string message = CodeBehindStringResolver.Resolve(
                    "IncorrectDiscretionValueMessage");
                MessageBox.Show(message);
                return;
            }
            IDictionary<string, long> distribution = GetDistribution();
            controlGraphicalRepresentation.DisplayDistribution(distribution);
        }

        private IDictionary<string, long> GetDistribution()
        {
            SigmaDistributor sigmaDistributor = 
                new SigmaDistributor(item.Model.GetDoubleValues().ToList(), discretion);
            return sigmaDistributor.Distribute();
        }

        private void ShowSourceInfo(object sender, RoutedEventArgs e)
        {
            AnalyticStatisticsItemSourceInfoWindow wnd = 
                new AnalyticStatisticsItemSourceInfoWindow(item);
            wnd.Show();
        }
    }
}

