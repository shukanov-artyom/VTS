using System;
using System.Windows;

namespace Agent.Common.Presentation.Error
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow()
        {
            InitializeComponent();
        }

        public ErrorWindow(string msg)
            : this()
        {
            controlErrorReporting.textBoxErrorMessage.Text = msg;
        }

        public ErrorWindow(Exception e, string msg)
            : this()
        {
            string errorMessage =
                String.Format(@"{0}
                                -------------------- Stack: -------------------
                                {1}", e.Message, e.StackTrace);
            controlErrorReporting.textBoxErrorMessage.Text = errorMessage;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
