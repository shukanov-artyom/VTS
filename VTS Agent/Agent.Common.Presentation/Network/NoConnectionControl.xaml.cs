using System;
using System.Windows;
using System.Windows.Controls;

namespace Agent.Common.Presentation.Network
{
    /// <summary>
    /// Interaction logic for NoConnectionControl.xaml
    /// </summary>
    public partial class NoConnectionControl : UserControl
    {
        public event EventHandler OkClicked;

        public NoConnectionControl()
        {
            InitializeComponent();
        }

        private void OnOkClicked(object sender, RoutedEventArgs e)
        {
            if (OkClicked != null)
            {
                OkClicked.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
