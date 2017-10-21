using System;
using System.Windows;

namespace Agent.Workspace
{
    /// <summary>
    /// Interaction logic for UnrecognizedDataExportTypeSelectionWindow.xaml
    /// </summary>
    public partial class UnrecognizedDataExportTypeSelectionWindow : Window
    {
        public UnrecognizedDataExportTypeSelectionWindow()
        {
            InitializeComponent();
        }

        public bool IsManually
        {
            get
            {
                return radioButtonIsManual.IsChecked.Value;
            }
        }

        public bool IsAutomatically
        {
            get
            {
                return radioButtonIsAutomatically.IsChecked.Value;
            }
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
