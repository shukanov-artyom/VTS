using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Presentation
{
    public partial class VehicleAnalyticModelControl : UserControl
    {
        private AnalyticItemViewModel selectedItem;

        public VehicleAnalyticModelControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            ApplicationSizeKeeper.AppResized += OnAppResized;
            TranslationManager.Instance.LanguageChanged += OnLanguageChanged;
            RefreshSize();
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                return;
            }
            DisplayElementDescription(selectedItem);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            RefreshSize();
        }

        private void OnAppResized(object sender, EventArgs e)
        {
            RefreshSize();
        }

        private void RefreshSize()
        {
            mainTreeView.Height = ApplicationSizeKeeper.Height - 260;
        }

        private void OnGridSelectedItemChanged(object sender, 
            RoutedPropertyChangedEventArgs<object> e)
        {
            selectedItem = e.NewValue as AnalyticItemViewModel;
            DisplayMarksHistory(selectedItem);
            DisplayElementDescription(selectedItem);
        }

        public void DisplayMarksHistory(AnalyticItemViewModel item)
        {
            if (item == null)
            {
                contentControlMarksHistory.Content = null;
                return;
            }
            MarksHistoryControl history =
                new MarksHistoryControl(item);
            contentControlMarksHistory.Content = history;
        }

        private void DisplayElementDescription(AnalyticItemViewModel item)
        {
            if (item == null)
            {
                textBlockItemInformation.Text = String.Empty;
                textBlockReliability.Text = String.Empty;
                stackPanelReliability.Visibility = Visibility.Collapsed;
                return;
            }
            textBlockItemInformation.Visibility = Visibility.Visible;
            string typeFormat = "AnalyticItemDescriptionText{0}{1}";
            string keyType = item.Model.GetType().Name;
            string additionalKey = item.Model.AdditionalInfo;
            textBlockItemInformation.Text = CodeBehindStringResolver.Resolve(
                String.Format(typeFormat, keyType, additionalKey));
            string key = String.Format("Reliability{0}DisplayName", 
                item.Model.Reliability);
            stackPanelReliability.Visibility = Visibility.Visible;
            textBlockReliability.Text = CodeBehindStringResolver.Resolve(key);
        }
    }
}
