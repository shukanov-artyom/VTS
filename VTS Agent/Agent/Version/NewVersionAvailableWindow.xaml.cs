using System;
using System.Windows;
using Agent.Common.Instance;
using Agent.Common.Presentation.Error;

namespace Agent.Version
{
    /// <summary>
    /// Interaction logic for NewVersionAvailableWindow.xaml
    /// </summary>
    public partial class NewVersionAvailableWindow : Window
    {
        private readonly string downloadLink;

        public NewVersionAvailableWindow()
        {
            InitializeComponent();
        }

        public NewVersionAvailableWindow(string downloadLink)
            : this()
        {
            this.downloadLink = downloadLink;
        }

        private void OnDownloadClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(downloadLink);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                {
                    ErrorWindow wnd = new ErrorWindow(noBrowser.Message);
                    wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
                    wnd.ShowDialog();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
