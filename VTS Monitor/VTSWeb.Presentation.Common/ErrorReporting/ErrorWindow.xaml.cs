using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Common.ErrorReporting
{
    public partial class ErrorWindow : ChildWindow
    {
        public ErrorWindow()
        {
            InitializeComponent();
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

