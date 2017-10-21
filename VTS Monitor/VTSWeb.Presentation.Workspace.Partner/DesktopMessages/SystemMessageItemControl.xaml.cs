using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Workspace.Partner.DesktopMessages
{
    public partial class SystemMessageItemControl : UserControl
    {
        public event EventHandler Click;

        public SystemMessageItemControl()
        {
            InitializeComponent();
            button.Click += OnButtonClicked;
        }

        public void SetCaption(string number)
        {
            textBlockContent.Text = number;
        }

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            if (Click != null)
            {
                Click.Invoke(this, e);
            }
        }
    }
}
